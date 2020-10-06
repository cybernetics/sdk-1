// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.NET.TestFramework;
using Microsoft.NET.TestFramework.Assertions;
using Microsoft.NET.TestFramework.Commands;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.NET.Publish.Tests
{
    public class GivenThatWeWantToPublishAnUnpublishableProject : SdkTest
    {
        public GivenThatWeWantToPublishAnUnpublishableProject(ITestOutputHelper log) : base(log)
        {
        }

        [Fact(Skip ="")]
        public void It_does_not_publish_to_the_publish_folder()
        {
            var helloWorldAsset = _testAssetsManager
                .CopyTestAsset("Unpublishable")
                .WithSource();

            var publishCommand = new PublishCommand(Log, helloWorldAsset.TestRoot);
            var publishResult = publishCommand.Execute();

            publishResult.Should().Pass();

            var publishDirectory = publishCommand.GetOutputDirectory();

            publishDirectory.Should().NotExist();
        }
    }
}
