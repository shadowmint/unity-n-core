# n-core

A unity package providing some basic unity helper scripts.

## Usage

See the individual source files for examples; most have tests embedded
at the end like this:

    #if UNITY_EDITOR

        /// Test function
        public class EventsTests : TestSuite
        {
            public void test_event_name()
            {
                var instance = new TestEvent1();
                Assert(instance.Name == "N.TestEvent1");
            }
        }

    #endif

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
