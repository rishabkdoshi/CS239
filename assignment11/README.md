# Question
Present a README file that describes how to input the function f, how to run the program, and how to understand the output. Submit three files, one for each program and one with the report.

## SIMON's algorithm.

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

# GROVER'S ALGORITHM

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
