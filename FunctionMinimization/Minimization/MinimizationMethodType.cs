using System.ComponentModel;

namespace FunctionMinimization.Minimization
{
    public enum MinimizationMethodType
    {
        [Description("Simple Gradient Descent (numerical, general purpose)")]
        SimpleGradientNum,

        [Description("Simple Gradient (task-optimized)")]
        SimpleGradient,

        [Description("Newton's (numerical, general purpose)")]
        NewtonsNum,

        [Description("Newton's (task-optimized)")]
        Newtons,
    }
}
