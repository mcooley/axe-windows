﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;

namespace Axe.Windows.Automation
{
    internal class ScanToolsBuilder : IScanToolsBuilder
    {
        private readonly IFactory _factory;
        private IOutputFileHelper _outputFileHelper;
        private IDPIAwareness _dpiAwareness;

        public ScanToolsBuilder(IFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public IScanToolsBuilder WithOutputDirectory(string outputDirectory)
        {
            _outputFileHelper = _factory.CreateOutputFileHelper(outputDirectory);
            return this;
        }

        public IScanToolsBuilder WithDPIAwareness(IDPIAwareness dpiAwareness)
        {
            _dpiAwareness = dpiAwareness;
            return this;
        }

        public IScanTools Build()
        {
            return new ScanTools(
                _outputFileHelper ?? _factory.CreateOutputFileHelper(null),
                _factory.CreateResultsAssembler(),
                _factory.CreateTargetElementLocator(),
                _factory.CreateAxeWindowsActions(),
                _dpiAwareness ?? _factory.CreateDPIAwareness());
        }
    } // class
} // namespace
