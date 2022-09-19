"use strict";
// basic types
let id = 5;
let company = 'Clemson';
let isPublished = true;
let x = 'Hello';
// arrays
let ids = [1, 2, 3, 4, 5];
let arr = [1, true, 'yo'];
// tuples
let person = [1, "danny", true];
let employee;
employee = [
    [1, "Danny"],
    [2, "Danny 2"],
    [3, "Danny 3"],
];
// unions
let pid = 22;
pid = '22';
//enums
var Direction1;
(function (Direction1) {
    Direction1[Direction1["Up"] = 1] = "Up";
    Direction1[Direction1["Down"] = 2] = "Down";
    Direction1[Direction1["Left"] = 3] = "Left";
    Direction1[Direction1["Right"] = 4] = "Right";
})(Direction1 || (Direction1 = {}));
var Direction2;
(function (Direction2) {
    Direction2["Up"] = "Up";
    Direction2["Down"] = "Down";
    Direction2["Left"] = "Left";
    Direction2["Right"] = "Right";
})(Direction2 || (Direction2 = {}));
const user = {
    id: 1,
    name: 'John'
};
// type assertion
let cid = 1;
// let customerId = <number> cid
let customerId = cid;
// functions
function addNum(x, y) {
    return x + y;
}
const user1 = {
    id: 1,
    name: 'John'
};
const add = (x, y) => x + y;
const subtract = (x, y) => x - y;
class Person {
    constructor(id, name) {
        this.id = id;
        this.name = name;
    }
    register() {
        return `${this.name} is now registered`;
    }
}
const danny = new Person(1, 'danny1');
const danny2 = new Person(2, 'danny2');
class Employee extends Person {
    constructor(id, name, position) {
        super(id, name);
        this.position = position;
    }
}
