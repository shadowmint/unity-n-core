#if N_CORE_TESTS
using System.Collections.Generic;
using System;
using N;
using UnityEditor;
using NUnit.Framework;
using N.Reflect;

class ValidatorTestTypeOne
{
    public string ignoredField = null;
    public ValidatorTestTypeTwo[] two;
    public ValidatorTestTypeThree three;
    public DateTime date = new DateTime();
}

class ValidatorTestTypeTwo
{
    public ValidatorTestTypeThree[] three;
    public int offset;
    public string value;
}

class ValidatorTestTypeThree
{
    public double value;
    public string str;
    public string ignoredField = null;
}

public class ValidatorTests : N.Tests.Test
{

    // Deliberately reuse validator to ensure robustness
    private Validator validator = null;
    public Validator Fixture()
    {
        if (validator == null)
        {
            validator = new Validator().IgnoreProperty("ignoredField").IgnoreType<DateTime>();
        }
        return validator;
    }

    [Test]
    public void test_validator_works()
    {
        var instance = new ValidatorTestTypeOne()
        {
            two = new ValidatorTestTypeTwo[] {
          new ValidatorTestTypeTwo() {
            three = new ValidatorTestTypeThree[] {
              new ValidatorTestTypeThree() {
                value = 10.0,
                str = ""
              },
              new ValidatorTestTypeThree() {
                value = 10.0,
                str = ""
              }
            },
            offset = 100,
            value = "Hello World"
          },
          new ValidatorTestTypeTwo() {
            three = new ValidatorTestTypeThree[] {
              new ValidatorTestTypeThree() {
                value = 10.0,
                str = ""
              }
            },
            offset = 100,
            value = "Hello World"
          },
        },
            three = new ValidatorTestTypeThree()
            {
                value = 10.0,
                str = ""
            }
        };

        Assert(Fixture().Validate(instance).IsOk);
    }

    [Test]
    public void test_validator_fails_on_null_array()
    {
        var instance = new ValidatorTestTypeOne()
        {
            two = null,
            three = new ValidatorTestTypeThree()
            {
                value = 10.0,
                str = ""
            }
        };

        var valid = Fixture().Validate(instance);
        Assert(valid.IsErr);

        var errors = valid.Err.Unwrap();
        Assert(errors.Length == 1);
        Assert(errors[0].name == "two");
    }

    [Test]
    public void test_validator_passes_on_empty_array()
    {
        var instance = new ValidatorTestTypeOne()
        {
            two = new ValidatorTestTypeTwo[] { },
            three = new ValidatorTestTypeThree()
            {
                value = 10.0,
                str = ""
            }
        };

        var valid = Fixture().Validate(instance);
        Assert(valid.IsOk);
    }

    [Test]
    public void test_validator_fails_on_empty_array_item()
    {
        var instance = new ValidatorTestTypeOne()
        {
            two = new ValidatorTestTypeTwo[] {
          new ValidatorTestTypeTwo() {
            three = new ValidatorTestTypeThree[] {
              new ValidatorTestTypeThree() {
                value = 10.0,
                str = ""
              },
              null,
              new ValidatorTestTypeThree() {
                value = 10.0
              }
            },
            offset = 100
          }
        },
            three = new ValidatorTestTypeThree()
            {
                value = 10.0,
                str = ""
            }
        };

        var valid = Fixture().Validate(instance);
        Assert(valid.IsErr);

        var errors = valid.Err.Unwrap();
        Assert(errors.Length == 3);
    }
}
#endif
