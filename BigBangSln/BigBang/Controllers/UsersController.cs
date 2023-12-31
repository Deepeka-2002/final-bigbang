﻿using BigBang.Interface;
using BigBang.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BigBang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUsers _userRepository;
        private readonly string _jwtSecret = "Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs03Hdx"; // Replace this with a secure secret key.

        public UsersController(IUsers userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetPendingUsers()
        {
            var customers = await _userRepository.GetPendingUsers();
            return Ok(customers);
        }

        [HttpGet("ActiveUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetActiveUsers()
        {
            var customers = await _userRepository.GetActiveUsers();
            return Ok(customers);
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(User user)
        {
            if (_userRepository == null)
            {
                return Problem("User repository is null.");
            }

            user.Password = Encrypt(user.Password);
            if(user.Role == "Agent")
            {
                user.Status = false;
            }
            else
            {
                user.Status = true;
            }

            var createdUser = await _userRepository.AddUser(user);

            // Generate JWT token with user details
            var token = GenerateJwtToken(createdUser);

            return Ok(token);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] User loginModel)
        {
            if (_userRepository == null)
            {
                return Problem("User repository is null.");
            }

            // Find the user by their email
            var existingUser = await _userRepository.GetUserByEmail(loginModel.EmailId);

            if (existingUser == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Decrypt the stored password and compare it with the provided password
            var decryptedPassword = Decrypt(existingUser.Password);
            if (loginModel.Password != decryptedPassword)
            {
                return Unauthorized("Invalid credentials");
            }

            if (!existingUser.Status)
            {
                return Unauthorized("Not approved");
            }
            // Passwords match, generate JWT token with user details
            var token = GenerateJwtToken(existingUser);

            // Return the token as part of the response
            return Ok(token);
        }

        [HttpPost("approve/{userId}")]
        public async Task<ActionResult> ApproveAgent(int userId)
        {
            if (_userRepository == null)
            {
                return Problem("User repository is null.");
            }

            var agent = await _userRepository.GetUserById(userId);

            if (agent == null)
            {
                return NotFound("Agent not found.");
            }

            if (agent.Role != "Agent")
            {
                return BadRequest("The user is not an agent.");
            }

            agent.Status = true;

            await _userRepository.UpdateUser(agent);

            return Ok("Agent approved successfully.");
        }


        [HttpPost("reject/{userId}")]
        public async Task<ActionResult> RejectUser(int userId)
        {
            if (_userRepository == null)
            {
                return Problem("User repository is null.");
            }

            var user = await _userRepository.GetUserById(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            await _userRepository.DeleteUser(user); // Delete the user from the database

            return Ok("User rejected successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteUserById(int id)
        {
            try
            {
                var customer = await _userRepository.DeleteUserById(id);
                return Ok(customer);
            }
            catch (ArithmeticException ex) 

            {
                return NotFound(ex.Message);
            }
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.EmailId),
             }),
                Expires = DateTime.UtcNow.AddDays(1), 
                SigningCredentials = credentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string Encrypt(string password)
        {
            // Example key and IV generation using hashing
            string passphrase = "your-passphrase";

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(passphrase));
                byte[] iv = sha256.ComputeHash(Encoding.UTF8.GetBytes(passphrase)).Take(16).ToArray();

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter writer = new StreamWriter(cryptoStream))
                            {
                                writer.Write(password);
                            }
                        }

                        byte[] encryptedData = memoryStream.ToArray();
                        return Convert.ToBase64String(encryptedData);
                    }
                }
            }
        }

        private string Decrypt(string encryptedPassword)
        {
            // Example key and IV generation using hashing
            string passphrase = "your-passphrase";

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(passphrase));
                byte[] iv = sha256.ComputeHash(Encoding.UTF8.GetBytes(passphrase)).Take(16).ToArray();

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    byte[] encryptedData = Convert.FromBase64String(encryptedPassword);

                    using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader reader = new StreamReader(cryptoStream))
                            {
                                return reader.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }
    }

}

