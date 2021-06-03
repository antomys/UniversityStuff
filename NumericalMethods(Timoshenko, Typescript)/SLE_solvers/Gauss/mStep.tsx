import matrixService = require('./matrixService')

type Array3 = [number,number,number] | []
type Array3x3 = [Array3, Array3, Array3]

const mStep = (currentMatrix: Array3x3, currentVector: Array3, rowIndex: number) : [Array3x3, Array3] =>  {
  const mMatrix : Array3x3 = createMMatrix(currentMatrix, rowIndex)
 // matrixService.printMatrix4x4(mMatrix)
  const newMatrix = matrixService.multiplyMatrixByMatrix(mMatrix, currentMatrix)
  const newVector = matrixService.multiplyMatrixByVector(mMatrix, currentVector)
  return [newMatrix, newVector]
}

const createMMatrix = (currentMatrix: Array3x3, rowIndex: number) => {
  const mMatrix : Array3x3 = [[],[],[]]

  for (let i = 0; i < 3; i++) {
    mMatrix[i] = [0,0,0]
  }

  for (let i = 0; i < rowIndex; i++){
    mMatrix[i][i] = 1
  }
  for (let i = rowIndex; i < 3; i++) {
    if (i === rowIndex) {
      mMatrix[i][i] = 1 / currentMatrix[i][i]
    }
    else {
      mMatrix[i][i] = 1
      mMatrix[i][rowIndex] = -(currentMatrix[i][rowIndex]/currentMatrix[rowIndex][rowIndex])
    }
  }
  console.log('________')
  console.log("m MATRIX:")
  matrixService.printMatrix4x4(mMatrix)
  return mMatrix
}

export {mStep}