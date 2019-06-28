# Fly
Fragment is an easy to use programming language written in `c# .net core`.
### Code
#### Assignments
You can assign variables using a non typed expression: `hello = "Hello world!"`.
Some other examples of using variables:
```
// Some examples
i = 0;
f = 1.1;
myarray = [1, 2, 3, 4];
mydict = ["hello":"world", "hi":"there"];
world = mydict["hello"];
```
#### Definition
To define a method you can use a definition statement:
```
myfunction (a, b) => {
	return a + b;
}
print(myfunction(1,5));
```
#### Using
To use the native methods you can import libraries. For now there's only the `base` library. You should always use this library for basic functionality: `use base;`
So a .fly file might look like:
```
use base;

// Some examples
i = 0; // Integer
f = 1.1; // Float
myarray = [1, 2, 3, 4]; // Array
mydict = ["hello":"world", "hi":"there"]; // Dictionary
world = mydict["hello"];
arrayitem = myarray[0];

myfunction (a, b) => {
	return a + b;
}
print(myfunction(1,5));
```
#### Statements
Fly has `if` `elif` (else if) `else` and `while` statements.
These statements look like:
```
i = 100;
while: i != 0 => {
	print(i);
	i--;
}
if: i == 0 => {
	print("i == 0");
}
elif: i == 1 => {
	print("i == 1");
}
else:=> {
	print("i is something else");
}
```

#### Base Library
The base library includes the following methods:
```
print(1,2,3); // Prints all parameters with '\n' at the end
printSingle(1,2,3); // Prints all parameters without '\n' at the end
name = "Name";
name.length(); // returns the given strings length
array = [1,2,3];
first(array); // -> 1
first("test"); // -> t
var firstTwo = array.take(2); // Takes the x items from an array.
```