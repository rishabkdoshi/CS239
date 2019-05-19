# Running the Code

### Question
Present a README file that describes how to input the function f, how to run the program, and how to understand the output.
Submit three files, one for each program and one with the report.

### Ans 

All the code in our files are parametrized by a FunctionObject. Any new function f can be defined via a FunctionObject. Also, we have extended this FunctionObject for Deutsch-Jozsa and Bernstein-Vazirani separately so that the user of this function has access to specific functions based on the type of algorithm being run.

We will explain the usage with the help of below examples:

## Deutsch-Jozsa

### Testing with a Driver
To test any new function, we have created a DriverObject that abstracts away all the internal workings of our code.


For example:
If we have the below function, that takes an input parameter x(bit-string of size 2) and returns 1 all the time. This is an example of a constant function.


```python
def fx(x):
	return 1
```

If we want to test this function and get the result of running the quantum circuit, all we need to do is.

```
DeutschJozsaDriver(2,fx,'CONSTANT').runAndVerifyQuantumCircuit()
```

This internally creates a functionObject and the oracle representing the fx and runs the quantum circuit and returns the results of the measurement.

It creates a DeutschJozsaFunction object as below. n,fx and ftype are the arguments to DeutschJozsaDriver.

```
functionObj = DeutschJozsaFunction(n,fx,fType)
```

with n being the size of the input bit-string to the function x and fType is "CONSTANT" or "BALANCED" and fx is the pointer to the function.

In the above example, the functionObject would be declared as below.

```
constantFunctionObj = DeutschJozsaFunction(2,fx,"CONSTANT")
```

The results returned are the function type and also the time taken to run the quantum circuit. In case a timeout occurs, then None values are returned.

### Random Deutsch-Jozsa Functions

If the user wants to create a BALANCED or CONSTANT function on an input bitstring of size n at **random** , they can just pass in empty values while creating the DeutschJozsaFunction object. Our constructor will randomly initialize a constant or a balanced function of the given input string size.

Ex:

```
randomDjFunctionObj = DeutschJozsaFunction(2)
```

We can inspect the type of the function, by invoking the `getType()` method on the function object as below.

```
randomDjFunctionObj()
```

To verify if it is constant or balanced, we would use the function

```
isBalancedOrConstant(randomDjFunctionObj)
```

This would run the quantum circuit and return the results and process it to identify function is CONSTANT or BALANCED.


## Bernstein Vazirani Functions


### Testing with a and b values
To test any new function of the form f(x) = a.x+b, we have created a BernsteinVaziraniFunction object that represents the function.


For example:
If we have a value as 101(n=3) and b value as 0. And our f(x) is a.x + b.
We can create a BernsteinVaziraniFunction as below


```
bvFunctionObj = BernsteinVaziraniFunction(3,'101',0)
```

Syntax:

```
bvFunctionObj = BernsteinVaziraniFunction(n,a,b)
```

n is no. of qubits that f operates on (equal to size of a), a is the bitstring in f(x) = a.x+b and b is the bit that is added in f(x). 


Using this bvFunctionObj, we can get the values of a and b from our quantum circuit as below.

```
a,b,timeTaken = constructBVFunction(bvFunctionObj)
```
This will return the a value obtained from the quantum circuit. The b value computed classically by applying fx on a bitstring of all zeros and the timetaken by the quantum circuit. In case of timeouts occuring, the function will return None values for all a,b,timeTaken

### Testing with a driver

We also allow the user to directly test the BernsteinVazirani algorithm by creating a BernsteinVaziraniDriver. This abstracts away all the internal workings of our code. All the user inputs is the value of n, to signify the no. of qubits and rest is taken care by our driver.

Ex:

```
BernsteinVaziraniDriver(1).runAndVerifyQuantumCircuit()
```

Generically 

```
BernsteinVaziraniDriver(n).runAndVerifyQuantumCircuit()
```

It initializes a BernsteinVaziraniFunction object with a set to a bitstring(size n) of random 1s and 0s, b set to 0 or 1 randomly. It then invokes the constructFunction method passing the object created and finally verifying that the values obtained for a and b are consistent with the internal functionObject.

## Common Util Methods

Some other util methods available to all FunctionObjects are:

1. `functionObj.verifyUf()` - Verify Uf matrix created conforms with the equation Uf|x>|b> = |x>|b+f(x)>
2. `functionObj.getUf()` - Returns the Uf matrix created for the the given function f(x)
3. `functionObj.getFunctionResult(x,helper)` - returns the result of applying f(x) as |x>|b+f(x)>. The returned value is a qubit vector in the standard basis.