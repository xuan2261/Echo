using System;
using System.Collections.Generic;
using System.Linq;
using AsmResolver.PE.DotNet.Cil;
using Echo.Concrete.Emulation;
using Echo.Concrete.Emulation.Dispatch;

namespace Echo.Platforms.AsmResolver.Emulation.Dispatch.Variables
{
    /// <summary>
    /// Provides a handler for instructions with a variation of the <see cref="CilOpCodes.Ldloc"/> operation code.
    /// </summary>
    public class LdLoc : FallThroughOpCodeHandler
    {
        /// <inheritdoc />
        public override IReadOnlyCollection<CilCode> SupportedOpCodes => new[]
        {
            CilCode.Ldloc, CilCode.Ldloc_0, CilCode.Ldloc_1, CilCode.Ldloc_2, CilCode.Ldloc_3, CilCode.Ldloc_S
        };

        /// <inheritdoc />
        public override DispatchResult Execute(ExecutionContext context, CilInstruction instruction)
        {
            var environment = context.GetService<ICilRuntimeEnvironment>();
            var variable = environment.Architecture
                .GetReadVariables(instruction)
                .First();
            
            if (!(variable is CilVariable))
                return new DispatchResult(new InvalidProgramException());
            
            context.ProgramState.Stack.Push(context.ProgramState.Variables[variable]);
            return base.Execute(context, instruction);
        }
    }
}