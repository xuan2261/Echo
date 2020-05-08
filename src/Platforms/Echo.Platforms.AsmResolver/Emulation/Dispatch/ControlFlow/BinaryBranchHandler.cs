using AsmResolver.PE.DotNet.Cil;
using Echo.Concrete.Emulation;
using Echo.Concrete.Values.ValueType;
using Echo.Platforms.AsmResolver.Emulation.Values.Cli;

namespace Echo.Platforms.AsmResolver.Emulation.Dispatch.ControlFlow
{
    /// <summary>
    /// Provides a base for all branching operation codes that pop two arguments from the stack.
    /// </summary>
    public abstract class BinaryBranchHandler : BranchHandler
    {
        /// <inheritdoc />
        protected override bool? VerifyCondition(ExecutionContext context, CilInstruction instruction)
        {
            var (left, right) = BinaryOperationHelper.PopBinaryOperationArguments(context);

            return (left, right) switch
            {
                (IntegerValue a, IntegerValue b) => VerifyCondition(context, a, b),
                (FValue a, FValue b) => VerifyCondition(context, a, b),
                (OValue a, OValue b) => VerifyCondition(context, a, b),
                _ => null,
            };
        }

        /// <summary>
        /// Determines whether the branch condition has been met, based on two integer values.
        /// </summary>
        /// <param name="context">The context in which the instruction is being executed in.</param>
        /// <param name="left">The left operand of the comparison.</param>
        /// <param name="right">The right operand of the comparison.</param>
        /// <returns><c>true</c> if the branch should be taken, <c>false</c> if not, and <c>null</c> if the conclusion
        /// is unknown.</returns>
        protected abstract bool? VerifyCondition(ExecutionContext context, IntegerValue left, IntegerValue right);
        
        /// <summary>
        /// Determines whether the branch condition has been met, based on two floating point values.
        /// </summary>
        /// <param name="context">The context in which the instruction is being executed in.</param>
        /// <param name="left">The left operand of the comparison.</param>
        /// <param name="right">The right operand of the comparison.</param>
        /// <returns><c>true</c> if the branch should be taken, <c>false</c> if not, and <c>null</c> if the conclusion
        /// is unknown.</returns>
        protected abstract bool? VerifyCondition(ExecutionContext context, FValue left, FValue right);
        
        /// <summary>
        /// Determines whether the branch condition has been met, based on two object references.
        /// </summary>
        /// <param name="context">The context in which the instruction is being executed in.</param>
        /// <param name="left">The left operand of the comparison.</param>
        /// <param name="right">The right operand of the comparison.</param>
        /// <returns><c>true</c> if the branch should be taken, <c>false</c> if not, and <c>null</c> if the conclusion
        /// is unknown.</returns>
        protected abstract bool? VerifyCondition(ExecutionContext context, OValue left, OValue right);
    }
}