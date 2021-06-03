import pStepService = require('./pStep')
import mStepService = require('./mStep')

type Array3 = [number,number,number] | []
type Array3x3 = [Array3, Array3, Array3]

const printMatrix4x4 = (matrix: Array3x3) =>
{
  for (let i = 0; i < 3; i++) {
    for (let j = 0; j < 3; j++) {
      process.stdout.write((matrix[i][j].toFixed(2)).padEnd(10))
    }
    console.log(  )
  }
}

const printVector = (vector: Array3) =>
{
  for (let i = 0; i < 3; i++) {
    console.log(vector[i].toFixed(2) + " ")
  }
}

const multiplyMatrixByMatrix = (firstMatrix: Array3x3, secondMatrix: Array3x3) =>
{
  let resultMatrix: Array3x3 = [[], [], []]

  for (let i = 0; i < 3; i++) {
    resultMatrix[i] = [0, 0, 0];
  }

  for (let i = 0; i < 3; i++) {
    for (let j = 0; j < 3; j++) {
      //console.log(`indices for result matrix: ${i}, ${j}`)
      //console.log('_______________________')
      for (let k = 0; k < 3; k++) {
        resultMatrix[i][j] += firstMatrix[i][k] * secondMatrix[k][j]
        //console.log(`first matrix element: ${firstMatrix[i][k]}, second matrix element: ${secondMatrix[k][j]}`)
        /*printMatrix(resultMatrix)*/
      }
    }
  }

  return resultMatrix
}

const multiplyMatrixByVector = (matrix: Array3x3, vector: Array3) =>
{
  const newVector: Array3 = new Array(3).fill(0) as Array3

  for (let i = 0; i < 3; i++) {
    for (let j = 0; j < 3; j++) {
      newVector[i] += matrix[i][j] * vector[j]
    }
  }
  return newVector
}

const backwardsSolving = (matrix: Array3x3, vector: Array3) =>
{
  const solution: Array3 = [0, 0, 0]
  for (let i = 2; i >= 0; i--) {
    let subtractionPart = 0
    for (let j = i; j < 3; j++) {
      subtractionPart += matrix[i][j] * solution[j]
    }
    solution[i] = vector[i] - subtractionPart

  }

  return solution
}

const solver = (currentMatrix: Array3x3, constantsVector: Array3) => {
  console.log ('matrix before gauss method:')
  printMatrix4x4(currentMatrix)
  console.log ('vector before gauss method:')
  printVector(constantsVector)


  for (let i = 0; i < 3; i++) {
    let [newMatrixP, newConstantsVectorP] : (Array3x3 | Array3)[] = pStepService.pStep(currentMatrix, constantsVector, i)
    console.log (`matrix after row swap #${i + 1}:`)
    printMatrix4x4(newMatrixP as Array3x3)
    console.log (`vector after row swap #${i + 1}:`)
    printVector(newConstantsVectorP as Array3)
    let [newMatrixM, newConstantsVectorM] : (Array3x3 | Array3)[] = mStepService.mStep(newMatrixP as  Array3x3, newConstantsVectorP as Array3, i)
    console.log (`matrix after normalizing #${i + 1}:`)
    printMatrix4x4(newMatrixM as Array3x3)
    console.log (`vector after normalizing #${i + 1}:`)
    printVector(newConstantsVectorM as Array3)
    currentMatrix = newMatrixM as Array3x3
    constantsVector = newConstantsVectorM as Array3
  }
  let solution : Array3 = backwardsSolving(currentMatrix, constantsVector)

  console.log('SOLUTION TO EQUATION:')
  printVector(solution)

}

export {
  multiplyMatrixByMatrix,
  multiplyMatrixByVector,
  printMatrix4x4,
  printVector,
  solver
}