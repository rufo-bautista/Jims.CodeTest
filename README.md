# Jims.CodeTest

Solution: 
- Jims.CodingTest.sln
Projects:
- src/Jims.CodingTest.Calculator: 
     - An AWS Lambda Function that receives inputs to calculate. 
       The input object is structured as { FirstNumber: float, SecondNumber : float, Operator int }. 
       An API Gateway was created to expose its functionality. 
       The API Gateway receives a post request, retrieves the input object from the body, 
       passes it to the lambda function and returns the result to the caller.

- src/Jims.CodingTest.Console: A Console project that consumes the API Gateway. 
     - It creates an input object, passess the object to the gateway, receives the result and dispalys the result to the console.
     Two paths:
          - The Main program has two paths. Depending on the value of the "CalculateHardCoded" node in the appsettings.json file, 
            the program either submits hard coded inputs to the gateway (CalculateHardCoded=true) or prompts the user to enter values for
            the input fields FirstNumber, SecondNumber and Operator before submitting it to the gateway (CalculateHardCoded=false).