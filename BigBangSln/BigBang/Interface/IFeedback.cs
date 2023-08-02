using BigBang.Models;

namespace BigBang.Interface
{
    public interface IFeedback
    {
        Task<List<Feedback>> GetFeedbacks();

        Task<List<Feedback>> AddFeedback(Feedback apps);

        Task<List<Feedback>?> UpdateFeedbackById(int id, Feedback apps);
        Task<List<Feedback>?> DeleteFeedbackById(int id);
    }
}
