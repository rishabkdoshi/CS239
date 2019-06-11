# Running the Code

### Question
Present a README file that describes how to input the function f, how to run the program, and how to understand the output.
Submit three files, one for each program and one with the report.

### Ans 

All the code in our files for DJ, BV ans Simon's are parametrized by a FunctionObject. Any new function f can be defined via a FunctionObject. Also, we have extended this FunctionObject for Deutsch-Jozsa, Bernstein-Vazirani and Simon's separately so that the user of this function has access to specific functions based on the type of algorithm being run.
Grover's uses lesser number of helper functions and is parameterized in a less extensive manner.

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

## Simon's

### Testing with a driver.

To test any new function, we have created a DriverObject that abstracts away all the internal workings of our code.


For example:
If we have the below function, that takes an input parameter inputString(bit-string of size 2) and returns the corresponding item in the dictionary. This is an example of a function with n=2 and s = '01' 


```python
def fx(inputString):
	fn_dict = {'00':'10','01':'10','10':'01','11':'01'}  
	return fn_dict[inputString]



```

If we want to test this function and get the result of running the quantum circuit, all we need to do is.

```
SimonsDriver(2).runAndVerifyQuantumCircuit()
```

This internally creates a functionObject and the oracle representing the fx and runs the quantum circuit and returns the results of the measurement.

It creates a SimonsFunction object as below. n,fntype are the arguments to SimonsDriver. fntype defaults to None if it is not specified.

```
functionObj = SimonsFunction(n,fnType=None)
```

with n being the size of the input bit-string to the function x and fntype being 1 or 2 if n=3(since 3 sample functions have been provided for testing when n=3)


The results returned are the function type and also the time taken to run the quantum circuit.

### Testing a generic function.
As seen above the input to the function is a functionObject. So, if there is a generic function fx as below.

```python

#s = '01'
def fx(inputString):
	fn_dict = {'00':'10','01':'10','10':'01','11':'01'}  
	return fn_dict[inputString]
```

We can create a function object with this fx as below.

```
functionObject = SimonsFunction(2,None, fx, '01')

#syntax
#SimonsFunction(SizeOfS, FunctionType,  functionPointer, SValue)
#FunctionType is only used to specify default example functions and should be None, when specifying our own function.
```

Using this created functionObject, we can get the S value as below

```
s,callsToQuantumCircuit,circuitRunTime = getS(functionObject)
```

The values returned are the s value, the no. of times the quantum circuit was run and the total runtime of the function.

# Grover's

### Goal

Present a README file that describes how to input the function f, how to run the program, and how to understand the output.

### Steps

1. User enters n. 
2. Function type is selected randomly: Only one possibility of x resulting in f(x)=1 OR one or more possibilities of x resulting in f(x)=1.
3. Z_f and -Z_0 are created of size 2^n x 2^n each. No helper bits involved.
4. H applied to |0^n>
5. Max iter calculated.
6. For that many iterations:<br>
	<pre>a. Z_f applied<br>
	b. H applied to all lines<br>
	c. -Z_0 applied.<br>
	d. H applied to all lines.</pre><br>
	
7. Measurement taken.

### Output Explanation

Results in the following form: {line: array ([trial_output_i for nTrials])}

Example 1: {0: array([0, 0, 0, 0, 0]), 1: array([1, 1, 1, 1, 0])} is |01>, |01>,|01>,|01>,|00> and number of Trials is 5.
Example 2: {0: array([0, 1]), 1: array([1, 0]), 2: array([1, 0])} is |011>, |100> and number of Trials is 2.

### Function types and explanation

1. Only one possibility of x resulting in f(x)=1 
	a. Random binary bit array of length n generated.
	b. Only x that is equal to a can have f(x)=1
2. One or more possibilities of x resulting in f(x)=1.
	a. Random binary bit array of length n generated such that only one 1 and rest all 0s.
	b. f(x)=1 if a.x=1.
-------------------------------------------------------------------------------------------------------------------


