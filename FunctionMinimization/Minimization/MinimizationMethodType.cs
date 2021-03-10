using System.ComponentModel;

namespace FunctionMinimization.Minimization
{
    public enum MinimizationMethodType
    {
        [Description("Simple Gradient Descent (numerical)")]
        SimpleGradientNum,

        [Description("Simple Gradient (task-optimized)")]
        SimpleGradient,

        [Description("Newton's (numerical)")]
        NewtonsNum,

        [Description("Newton's (task-optimized)")]
        Newtons,
    }
}
