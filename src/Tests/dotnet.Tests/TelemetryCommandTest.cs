﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Cli.Telemetry;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.DotNet.Tools.Test.Utilities;
using System.Collections.Generic;
using System;
using Xunit;
using Microsoft.NET.TestFramework;
using Xunit.Abstractions;

namespace Microsoft.DotNet.Tests
{
    public class TelemetryCommandTests : SdkTest
    {
        private readonly FakeRecordEventNameTelemetry _fakeTelemetry;

        public string EventName { get; set; }

        public IDictionary<string, string> Properties { get; set; }

        public TelemetryCommandTests(ITestOutputHelper log) : base(log)
        {
            _fakeTelemetry = new FakeRecordEventNameTelemetry();
            TelemetryEventEntry.Subscribe(_fakeTelemetry.TrackEvent);
            TelemetryEventEntry.TelemetryFilter = new TelemetryFilter(Sha256Hasher.HashWithNormalizedCasing);
        }

        [Fact(Skip ="")]
        public void NoTelemetryIfCommandIsInvalid()
        {
            string[] args = { "publish", "-r"};
            Action a = () => { Cli.Program.ProcessArgs(args); };
            a.ShouldNotThrow<ArgumentOutOfRangeException>();
        }

        [Fact(Skip ="")]
        public void NoTelemetryIfCommandIsInvalid2()
        {
            string[] args = { "restore", "-v" };
            Action a = () => { Cli.Program.ProcessArgs(args); };
            a.ShouldNotThrow<ArgumentOutOfRangeException>();
        }

        [Fact(Skip ="")]
        public void TopLevelCommandNameShouldBeSentToTelemetry()
        {
            string[] args = { "help" };
            Cli.Program.ProcessArgs(args);

            _fakeTelemetry.LogEntries.Should().Contain(e => e.EventName == "toplevelparser/command" &&
                              e.Properties.ContainsKey("verb") &&
                              e.Properties["verb"] == Sha256Hasher.Hash("HELP"));
        }

        [Fact(Skip ="")]
        public void DotnetNewCommandFirstArgumentShouldBeSentToTelemetry()
        {
            const string argumentToSend = "console";
            string[] args = { "new", argumentToSend };
            Cli.Program.ProcessArgs(args);
            _fakeTelemetry
                .LogEntries.Should()
                .Contain(e => e.EventName == "sublevelparser/command" &&
                              e.Properties.ContainsKey("argument") &&
                              e.Properties["argument"] == Sha256Hasher.Hash(argumentToSend.ToUpper()) &&
                              e.Properties.ContainsKey("verb") &&
                              e.Properties["verb"] == Sha256Hasher.Hash("NEW"));
        }

        [Fact(Skip ="")]
        public void DotnetHelpCommandFirstArgumentShouldBeSentToTelemetry()
        {
            const string argumentToSend = "something";
            string[] args = { "help", argumentToSend };
            Cli.Program.ProcessArgs(args);
            _fakeTelemetry
                .LogEntries.Should()
                .Contain(e => e.EventName == "sublevelparser/command" &&
                              e.Properties.ContainsKey("argument") &&
                              e.Properties["argument"] == Sha256Hasher.Hash(argumentToSend.ToUpper()) &&
                              e.Properties.ContainsKey("verb") &&
                              e.Properties["verb"] == Sha256Hasher.Hash("HELP"));
        }

        [Fact(Skip ="")]
        public void DotnetAddCommandFirstArgumentShouldBeSentToTelemetry()
        {
            const string argumentToSend = "package";
            string[] args = { "add", argumentToSend, "aPackageName" };
            Cli.Program.ProcessArgs(args);
            _fakeTelemetry
                .LogEntries.Should()
                .Contain(e => e.EventName == "sublevelparser/command" &&
                              e.Properties.ContainsKey("argument") &&
                              e.Properties["argument"] == Sha256Hasher.Hash(argumentToSend.ToUpper()) &&
                              e.Properties.ContainsKey("verb") &&
                              e.Properties["verb"] == Sha256Hasher.Hash("ADD"));
        }

        [Fact(Skip ="")]
        public void DotnetAddCommandFirstArgumentShouldBeSentToTelemetry2()
        {
            const string argumentToSend = "reference";
            string[] args = { "add", argumentToSend, "aPackageName" };
            Cli.Program.ProcessArgs(args);
            _fakeTelemetry
                .LogEntries.Should()
                .Contain(e => e.EventName == "sublevelparser/command" &&
                              e.Properties.ContainsKey("argument") &&
                              e.Properties["argument"] == Sha256Hasher.Hash(argumentToSend.ToUpper()) &&
                              e.Properties.ContainsKey("verb") &&
                              e.Properties["verb"] == Sha256Hasher.Hash("ADD"));
        }

        [Fact(Skip ="")]
        public void DotnetRemoveCommandFirstArgumentShouldBeSentToTelemetry()
        {
            const string argumentToSend = "package";
            string[] args = { "remove", argumentToSend, "aPackageName" };
            Cli.Program.ProcessArgs(args);
            _fakeTelemetry
                .LogEntries.Should()
                .Contain(e => e.EventName == "sublevelparser/command" &&
                              e.Properties.ContainsKey("argument") &&
                              e.Properties["argument"] == Sha256Hasher.Hash(argumentToSend.ToUpper()) &&
                              e.Properties.ContainsKey("verb") &&
                              e.Properties["verb"] == Sha256Hasher.Hash("REMOVE"));
        }

        [Fact(Skip ="")]
        public void DotnetListCommandFirstArgumentShouldBeSentToTelemetry()
        {
            const string argumentToSend = "reference";
            string[] args = { "list", argumentToSend, "aPackageName" };
            Cli.Program.ProcessArgs(args);
            _fakeTelemetry
                .LogEntries.Should()
                .Contain(e => e.EventName == "sublevelparser/command" && e.Properties.ContainsKey("argument") &&
                              e.Properties["argument"] == Sha256Hasher.Hash(argumentToSend.ToUpper()) &&
                              e.Properties.ContainsKey("verb") &&
                              e.Properties["verb"] == Sha256Hasher.Hash("LIST"));
        }

        [Fact(Skip ="")]
        public void DotnetSlnCommandFirstArgumentShouldBeSentToTelemetry()
        {
            const string argumentToSend = "list";
            string[] args = { "sln", "aSolution", argumentToSend };
            Cli.Program.ProcessArgs(args);
            _fakeTelemetry
                .LogEntries.Should()
                .Contain(e => e.EventName == "sublevelparser/command" &&
                              e.Properties.ContainsKey("argument") &&
                              e.Properties["argument"] == Sha256Hasher.Hash(argumentToSend.ToUpper()) &&
                              e.Properties.ContainsKey("verb") &&
                              e.Properties["verb"] == Sha256Hasher.Hash("SLN"));
        }

        [Fact(Skip ="")]
        public void DotnetNugetCommandFirstArgumentShouldBeSentToTelemetry()
        {
            const string argumentToSend = "push";

            string[] args = { "nuget", argumentToSend, "path" };

            Cli.Program.ProcessArgs(args);
            _fakeTelemetry
                .LogEntries.Should()
                .Contain(e => e.EventName == "sublevelparser/command" &&
                              e.Properties.ContainsKey("argument") &&
                              e.Properties["argument"] == Sha256Hasher.Hash(argumentToSend.ToUpper()) &&
                              e.Properties.ContainsKey("verb") &&
                              e.Properties["verb"] == Sha256Hasher.Hash("NUGET"));
        }

        [Fact(Skip ="")]
        public void DotnetNewCommandLanguageOpinionShouldBeSentToTelemetry()
        {
            const string optionKey = "language";
            const string optionValueToSend = "c#";
            string[] args = { "new", "console", "--" + optionKey, optionValueToSend };
            Cli.Program.ProcessArgs(args);
            _fakeTelemetry
                .LogEntries.Should()
                .Contain(e => e.EventName == "sublevelparser/command" && e.Properties.ContainsKey(optionKey) &&
                              e.Properties[optionKey] == Sha256Hasher.Hash(optionValueToSend.ToUpper()) &&
                              e.Properties.ContainsKey("verb") &&
                              e.Properties["verb"] == Sha256Hasher.Hash("NEW"));
        }

        [Fact(Skip ="")]
        public void AnyDotnetCommandVerbosityOpinionShouldBeSentToTelemetry()
        {
            const string optionKey = "verbosity";
            const string optionValueToSend = "minimal";
            string[] args = { "restore", "--" + optionKey, optionValueToSend };
            Cli.Program.ProcessArgs(args);
            _fakeTelemetry
                .LogEntries.Should()
                .Contain(e => e.EventName == "sublevelparser/command" &&
                              e.Properties.ContainsKey(optionKey) &&
                              e.Properties[optionKey] == Sha256Hasher.Hash(optionValueToSend.ToUpper()) &&
                              e.Properties.ContainsKey("verb") &&
                              e.Properties["verb"] == Sha256Hasher.Hash("RESTORE"));
        }

        [Fact(Skip ="")]
        public void DotnetBuildAndPublishCommandOpinionsShouldBeSentToTelemetry()
        {
            const string optionKey = "configuration";
            const string optionValueToSend = "Debug";
            string[] args = { "build", "--" + optionKey, optionValueToSend };
            Cli.Program.ProcessArgs(args);
            _fakeTelemetry
                .LogEntries.Should()
                .Contain(e => e.EventName == "sublevelparser/command" &&
                              e.Properties.ContainsKey(optionKey) &&
                              e.Properties[optionKey] == Sha256Hasher.Hash(optionValueToSend.ToUpper()) &&
                              e.Properties.ContainsKey("verb") &&
                              e.Properties["verb"] == Sha256Hasher.Hash("BUILD"));
        }

        [Fact(Skip ="")]
        public void DotnetPublishCommandRuntimeOpinionsShouldBeSentToTelemetry()
        {
            const string optionKey = "runtime";
            const string optionValueToSend = "win10-x64";
            string[] args = { "publish", "--" + optionKey, optionValueToSend };
            Cli.Program.ProcessArgs(args);
            _fakeTelemetry
                .LogEntries.Should()
                .Contain(e => e.EventName == "sublevelparser/command" &&
                              e.Properties.ContainsKey(optionKey) &&
                              e.Properties[optionKey] == Sha256Hasher.Hash(optionValueToSend.ToUpper()) &&
                              e.Properties.ContainsKey("verb") &&
                              e.Properties["verb"] == Sha256Hasher.Hash("PUBLISH"));
        }

        [Fact(Skip ="")]
        public void DotnetBuildAndPublishCommandOpinionsShouldBeSentToTelemetryWhenThereIsMultipleOption()
        {
            string[] args = { "build", "--configuration", "Debug", "--runtime", "osx.10.11-x64" };
            Cli.Program.ProcessArgs(args);
            _fakeTelemetry
                .LogEntries.Should()
                .Contain(e => e.EventName == "sublevelparser/command" && e.Properties.ContainsKey("configuration") &&
                              e.Properties["configuration"] == Sha256Hasher.Hash("DEBUG") &&
                              e.Properties.ContainsKey("verb") &&
                              e.Properties["verb"] == Sha256Hasher.Hash("BUILD"));

            _fakeTelemetry
                .LogEntries.Should()
                .Contain(e => e.EventName == "sublevelparser/command" && e.Properties.ContainsKey("runtime") &&
                              e.Properties["runtime"] == Sha256Hasher.Hash("OSX.10.11-X64") &&
                              e.Properties.ContainsKey("verb") &&
                              e.Properties["verb"] == Sha256Hasher.Hash("BUILD"));
        }

        [Fact(Skip ="")]
        public void DotnetRunCleanTestCommandOpinionsShouldBeSentToTelemetryWhenThereIsMultipleOption()
        {
            string[] args = { "clean", "--configuration", "Debug", "--framework", "netcoreapp1.0" };
            Cli.Program.ProcessArgs(args);
            _fakeTelemetry
                .LogEntries.Should()
                .Contain(e => e.EventName == "sublevelparser/command" && e.Properties.ContainsKey("configuration") &&
                              e.Properties["configuration"] == Sha256Hasher.Hash("DEBUG") &&
                              e.Properties.ContainsKey("verb") &&
                              e.Properties["verb"] == Sha256Hasher.Hash("CLEAN"));

            _fakeTelemetry
                .LogEntries.Should()
                .Contain(e => e.EventName == "sublevelparser/command" && e.Properties.ContainsKey("framework") &&
                              e.Properties["framework"] == Sha256Hasher.Hash("NETCOREAPP1.0") &&
                              e.Properties.ContainsKey("verb") &&
                              e.Properties["verb"] == Sha256Hasher.Hash("CLEAN"));
        }

        [WindowsOnlyFact(Skip ="")]
        public void InternalreportinstallsuccessCommandCollectExeNameWithEventname()
        {
            FakeRecordEventNameTelemetry fakeTelemetry = new FakeRecordEventNameTelemetry();
            string[] args = { "c:\\mypath\\dotnet-sdk-latest-win-x64.exe" };

            InternalReportinstallsuccess.ProcessInputAndSendTelemetry(args, fakeTelemetry);

            fakeTelemetry
                .LogEntries.Should()
                .Contain(e => e.EventName == "install/reportsuccess" && e.Properties.ContainsKey("exeName") &&
                              e.Properties["exeName"] == Sha256Hasher.Hash("DOTNET-SDK-LATEST-WIN-X64.EXE"));
        }

        [Fact(Skip ="")]
        public void InternalreportinstallsuccessCommandIsRegisteredInBuiltIn()
        {
            BuiltInCommandsCatalog.Commands.Should().ContainKey("internal-reportinstallsuccess");
        }

        [Fact(Skip ="")]
        public void ExceptionShouldBeSentToTelemetry()
        {
            Exception caughtException = null;
            try
            {
                string[] args = { "build" };
                Cli.Program.ProcessArgs(args);
                throw new ArgumentException("test exception");
            }
            catch (Exception ex)
            {
                caughtException = ex;
                TelemetryEventEntry.SendFiltered(ex);
            }

            var exception = new Exception();
            _fakeTelemetry
                 .LogEntries.Should()
                 .Contain(e => e.EventName == "mainCatchException/exception" &&
                               e.Properties.ContainsKey("exceptionType") &&
                               e.Properties["exceptionType"] == "System.ArgumentException" &&
                               e.Properties.ContainsKey("detail") &&
                               e.Properties["detail"].Contains(caughtException.StackTrace));
        }
    }
}
