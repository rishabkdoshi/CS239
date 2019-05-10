import numpy as np
N = 2
def f(x):
	# print(x, type(x))
	if(x =="01" or x=="10"):
		return 1
	else:
		return 0
def f_1(x):
	return 1

def f_2(x):
	return 0

def createUf(f):
	indices_dict = {}
	counter = 0
	Uf = np.zeros((2**(N+1),2**(N+1)))
	print(Uf)
	bfx = 0

	b = 0
	for i in range(2**N):
		binary_form = ('{0:0'+str(N)+'b}').format(i) + str(b)
		indices_dict[binary_form] = counter
		counter += 1
		# print(binary_form)

	b = 1
	for i in range(2**N):
		binary_form = ('{0:0'+str(N)+'b}').format(i) + str(b)
		indices_dict[binary_form] = counter
		counter += 1
		# print(binary_form)

	print(indices_dict)
	for key,val in indices_dict.items():
		x = key[0:N]
		fx = str(f(x))
		b = key[N]
		if(b==fx):
			bfx = '0'
		else:
			bfx = '1'
			# print(bfx)
		target = x + bfx
		# print('t',target)
		target_index = indices_dict[target]
		# print(val,target_index)
		Uf[val][target_index] = 1
	return Uf



Uf = createUf(f)
print(Uf)

