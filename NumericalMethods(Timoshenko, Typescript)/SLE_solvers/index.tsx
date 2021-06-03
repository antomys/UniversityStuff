import Gauss = require('./Gauss/matrixService')
import SquareRoot = require('./SquareRoot/matrixService')


type Array4 = [number, number, number, number] | []
type Array4x4 = [Array4, Array4, Array4, Array4]
type Array3 = [number, number, number] | []
type Array3x3 = [Array3, Array3, Array3]


let currentMatrixGauss: Array3x3 =
  [
    [-1, 3, 6],
    [3, -5, -4],
    [-6, -4, -4]
  ]

let constantsVectorGauss: Array3 = [-9, 3, 4]

Gauss.solver(currentMatrixGauss, constantsVectorGauss)

/*
console.log('__________________________________________________')

let currentMatrixSquare : Array3x3 =
[[4, 2, 2],
[2, 2, 3],
[2, 3, 1]]

let constantsVectorSquare : Array3 = [2, 2, 3]

SquareRoot.solver(currentMatrixSquare, constantsVectorSquare)

*/
