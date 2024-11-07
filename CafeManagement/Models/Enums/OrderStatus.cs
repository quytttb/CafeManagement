namespace CafeManagement.Models.Enums;

// Class này dùng để định nghĩa các trạng thái của Order
public enum OrderStatus
{
    Default = 0,
    Completed = 1,
    Waiting = 2,
    Problem = -1,
    OtherProblem = -2
}