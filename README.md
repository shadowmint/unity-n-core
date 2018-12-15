# n-core

A unity package providing some basic unity helper scripts.

**Depreciated**

This package is depreciated and will no longer receive updates; in general this sort
of 'kitchen sink' package should be avoided; it is replaced by the smaller packages
with specific contents listed on:

https://github.com/shadowmint/unity-packages

## Usage

See the tests in the `Editor/` folder for each class for usage examples.

## Install

From your unity project folder:

    npm init
    npm install shadowmint/unity-n-core --save
    echo Assets/packages >> .gitignore
    echo Assets/packages.meta >> .gitignore

The package and all its dependencies will be installed in
your Assets/packages folder.

## Development

Setup and run tests:

    npm install
    cd test
    npm install
    gulp

### Tests

All tests are wrapped in `#if ...` blocks to prevent test spam.

You can enable tests in: Player settings > Other Settings > Scripting Define Symbols

The test key for this package is: N_CORE_TESTS
