using System;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.AnalysisCommands;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Commands;
using static StaadPro.Interop.Services.Commands.AnalysisCommandOptionValidation;
using static StaadPro.Interop.Services.Property.PropertyValueReader;

namespace StaadPro.Interop.Services.Commands
{
    // Shared installed 2024-2026 command contracts; no artificial year-specific service copies.
    internal sealed class CommandsService
    {
        private readonly OpenStaadWrapper wrapper;
        private readonly object native;
        private readonly object gate = new object ();
        internal CommandsService(OpenStaadWrapper wrapper, object native)
        {
            this.wrapper = wrapper;
            this.native = native;
        }

        private dynamic Binding()
        {
            if (wrapper.RawOpenStaad == null || !ReferenceEquals((object)wrapper.RawCommands, native))
                throw new InvalidOperationException("The Commands binding is disposed or replaced. Reacquire wrapper.Commands.");
            return native;
        }

        private CommandSubmissionResult Run(Func<dynamic, CommandSubmissionResult> call)
        {
            lock (gate)
            {
                dynamic p = Binding();
                var result = call(p);
                Binding();
                return result;
            }
        }

        private CommandSubmissionResult Void(string op, Action<dynamic> call) => Run(p =>
        {
            call(p);
            return new CommandSubmissionResult(op, CommandSubmissionStatus.InvokedWithoutAcknowledgement);
        });
        private static CommandSubmissionResult Ack(string op, object raw)
        {
            bool accepted = Flag(raw, op);
            return new CommandSubmissionResult(op, accepted ? CommandSubmissionStatus.Succeeded : op == "DeleteAllAnalysisCommands" ? CommandSubmissionStatus.NoCommandsPresent : CommandSubmissionStatus.NativeFailure, raw);
        }

        private static void Printing(PrintOption printOption, bool modeShapes = true)
        {
            EnumValue(printOption, nameof(printOption));
            if (!modeShapes && printOption == PrintOption.PrintModeShapes)
                throw new ArgumentOutOfRangeException("printOption", "Mode-shape printing is reserved for this operation.");
        }

        internal CommandSubmissionResult PerformAnalysis(PrintOption printOption)
        {
            Printing(printOption);
            return Void("PerformAnalysis", p => p.PerformAnalysis((int)printOption));
        }

        internal CommandSubmissionResult PerformPDeltaAnalysisNoConverge(int numberOfIterations, PrintOption printOption)
        {
            AnalysisCommandOptionValidation.Positive(numberOfIterations, nameof(numberOfIterations));
            Printing(printOption);
            return Void("PerformPDeltaAnalysisNoConverge", p => p.PerformPDeltaAnalysisNoConverge(numberOfIterations, (int)printOption));
        }

        internal CommandSubmissionResult PerformPDeltaAnalysisEx(int numberOfIterations, PrintOption printOption, PDeltaDisplacementEffect displacementEffect, bool includeGeometricStiffness)
        {
            AnalysisCommandOptionValidation.Positive(numberOfIterations, nameof(numberOfIterations));
            Printing(printOption);
            EnumValue(displacementEffect, nameof(displacementEffect));
            return Void("PerformPDeltaAnalysisEx", p => p.PerformPDeltaAnalysisEx(numberOfIterations, (int)printOption, (int)displacementEffect, (short)(includeGeometricStiffness ? 1 : 0)));
        }

        internal CommandSubmissionResult PerformPDeltaAnalysis(PDeltaAnalysisOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            options.Validate();
            return PerformPDeltaAnalysisEx(options.NumberOfIterations, options.PrintOption, options.DisplacementEffect, options.IncludeGeometricStiffness);
        }

        internal CommandSubmissionResult PerformCableAnalysis(int numberOfIterations, PrintOption printOption)
        {
            AnalysisCommandOptionValidation.Positive(numberOfIterations, nameof(numberOfIterations));
            Printing(printOption, false);
            return Void("PerformCableAnalysis", p => p.PerformCableAnalysis(numberOfIterations, (int)printOption));
        }

        internal CommandSubmissionResult PerformCableAnalysisEx(CableAnalysisOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            options.Validate();
            var method = options.Method;
            var printing = options.PrintOption;
            Printing(printing, false);
            object flags = options.CreateAdditionalOptionsArray(), parameters = options.CreateParameterArray();
            return Run(p => Ack("PerformCableAnalysisEx", (object)p.PerformCableAnalysisEx((short)(method == CableAnalysisMethod.Advanced ? 1 : 0), flags, parameters, (int)printing)));
        }

        internal CommandSubmissionResult PerformBucklingAnalysis(int maximumSteps, PrintOption printOption)
        {
            AnalysisCommandOptionValidation.Positive(maximumSteps, nameof(maximumSteps));
            Printing(printOption, false);
            return Void("PerformBucklingAnalysis", p => p.PerformBucklingAnalysis(maximumSteps, (int)printOption));
        }

        internal CommandSubmissionResult PerformBucklingAnalysisEx(BucklingAnalysisMethod method, int maximumSteps, PrintOption printOption)
        {
            EnumValue(method, nameof(method));
            AnalysisCommandOptionValidation.Positive(maximumSteps, nameof(maximumSteps));
            Printing(printOption, false);
            return Run(p => Ack("PerformBucklingAnalysisEx", (object)p.PerformBucklingAnalysisEx((int)method, maximumSteps, (int)printOption)));
        }

        internal CommandSubmissionResult PerformDirectAnalysis(DirectAnalysisOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            options.Validate();
            var format = options.DesignFormat;
            var printing = options.PrintOption;
            var parameters = options.CreateParameterArray();
            var flags = options.CreateAdditionalOptionsArray();
            return Run(p => Ack("PerformDirectAnalysis", (object)p.PerformDirectAnalysis((int)format, parameters, flags, (int)printing)));
        }

        internal CommandSubmissionResult PerformNonlinearAnalysisEx(NonlinearAnalysisOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            options.Validate();
            var printing = options.PrintOption;
            var arc = options.ArcLength;
            var iterations = options.MaximumIterations;
            var tolerance = options.ConvergenceTolerance;
            var steps = options.LoadSteps;
            var rebuild = options.RebuildFrequency;
            short geometric = (short)(options.IncludeGeometricStiffness ? 1 : 0);
            object target = options.CreateDisplacementTargetArray();
            return Run(p => Ack("PerformNonlinearAnalysisEx", (object)p.PerformNonlinearAnalysisEx((int)printing, arc, iterations, tolerance, steps, (int)rebuild, geometric, target)));
        }

        internal CommandSubmissionResult SetFloorDiaphragmBaseCommand(double baseElevation)
        {
            Finite(baseElevation, nameof(baseElevation));
            return Run(p => Ack("SetFloorDiaphragmBaseCommand", (object)p.SetFloorDiaphragmBaseCommand(baseElevation)));
        }

        internal CommandSubmissionResult DeleteFloorDiaphragmBaseCommand() => Run(p => Ack("DeleteFloorDiaphragmBaseCommand", (object)p.DeleteFloorDiaphragmBaseCommand()));
        internal CommandSubmissionResult SetCheckSoftStoryCommand(SoftStoryDesignCode designCode)
        {
            EnumValue(designCode, nameof(designCode));
            return Run(p => Ack("SetCheckSoftStoryCommand", (object)p.SetCheckSoftStoryCommand((int)designCode)));
        }

        internal CommandSubmissionResult DeleteCheckSoftStoryCommand() => Run(p => Ack("DeleteCheckSoftStoryCommand", (object)p.DeleteCheckSoftStoryCommand()));
        internal CommandSubmissionResult SetCheckIrregularitiesCommand(IrregularitiesDesignCode designCode)
        {
            EnumValue(designCode, nameof(designCode));
            return Run(p => Ack("SetCheckIrregularitiesCommand", (object)p.SetCheckIrregularitiesCommand((int)designCode)));
        }

        internal CommandSubmissionResult DeleteCheckIrregularitiesCommand() => Run(p => Ack("DeleteCheckIrregularitiesCommand", (object)p.DeleteCheckIrregularitiesCommand()));
        internal CommandSubmissionResult DeleteAllAnalysisCommands() => Run(p => Ack("DeleteAllAnalysisCommands", (object)p.DeleteAllAnalysisCommands()));
    }
}
