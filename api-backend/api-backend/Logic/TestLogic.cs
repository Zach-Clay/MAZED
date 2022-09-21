using System;
using api_backend.Models;

namespace api_backend.Logic
{
    public class TestLogic
    {
        public static TestValues getValues()
        {
            //create the values from the Models folder
            TestValues values = new TestValues();
            values.Value1 = "My Name is Zach";
            values.Value2 = "Hello World";

            //return the values
            return values;
        }


    }

}
