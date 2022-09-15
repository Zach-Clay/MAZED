// basic types
let id: number = 5
let company: string = 'Clemson'
let isPublished: boolean = true
let x: any = 'Hello'

// arrays
let ids: number[] = [1,2,3,4,5]
let arr: any[] = [1, true, 'yo']

// tuples
let person: [number, string, boolean] = [1, "danny", true]
let employee: [number, string][]

employee = [
    [1, "Danny"],
    [2, "Danny 2"],
    [3, "Danny 3"],
]

// unions
let pid: string | number = 22
pid = '22'

//enums
enum Direction1{
    Up = 1,
    Down,
    Left,
    Right
}

enum Direction2{
    Up = 'Up',
    Down = 'Down',
    Left = 'Left',
    Right = 'Right'
}

// objects
type User = {
    id: number,
    name: string
}

const user: User = {
    id: 1,
    name: 'John'
}

// type assertion
let cid: any = 1
// let customerId = <number> cid
let customerId = cid as number


// functions
function addNum(x: number, y: number): number{
    return x + y
}


// interfaces
interface UserInterface  {
    readonly id: number,
    name: string,
    age?: number
}

const user1: UserInterface = {
    id: 1,
    name: 'John'
}

interface MathFunc{
    (x: number, y:number): number
}

const add: MathFunc = (x:number, y:number): number => x+y
const subtract: MathFunc = (x:number, y:number): number => x-y


// classes
interface PersonInterface  {
    id: number,
    name: string,
    register(): string
}

class Person implements PersonInterface{
    id: number
    name: string

    constructor(id:number, name:string){
        this.id = id
        this.name = name
    }

    register(){
        return `${this.name} is now registered`
    }
}

const danny = new Person(1, 'danny1')
const danny2 = new Person(2, 'danny2')

class Employee extends Person{
    position: string

    constructor(id:number, name:string, position:string){
        super(id, name)
        this.position = position
    }
}