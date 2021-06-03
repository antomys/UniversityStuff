import matrixService = require('./matrixService')

type Array3 = [number,number,number] | []
type Array3x3 = [Array3, Array3, Array3]

const pStep = (currentMatrix: Array3x3, constantsVector: Array3, index: number) => {
  let maxRow: number = -1
  maxRow = findMax(currentMatrix, index).ind
  //console.log('maxRow index:', maxRow)
  console.log('keys', matrixService.multiplyMatrixByMatrix)
  const pMatrix : Array3x3 = createPMatrix(currentMatrix, maxRow, index)
  //matrixService.printMatrix4x4(pMatrix)
  console.log('________')
  console.log("P MATRIX:")
  matrixService.printMatrix4x4(pMatrix)
  const updatedMatrix : Array3x3 = matrixService.multiplyMatrixByMatrix(pMatrix, currentMatrix)
  const updatedVector : Array3 = matrixService.multiplyMatrixByVector(pMatrix, constantsVector)
  return [updatedMatrix, updatedVector]
}

const findMax = (currentMatrix: Array3x3, columnIndex: number) => {
  const column : Array3 = [0,0,0]
  for (let i = 0; i < 3; i++) {
    column[i] = currentMatrix[i][columnIndex]
  }
  return column.reduce((currMaxPair: {val: number, ind: number}, currEl: number, currInd: number) => {
    return (currMaxPair.val < currEl)
    ? {val: currEl, ind: currInd}
    : currMaxPair
  }, {val: Number.MIN_VALUE, ind: -1})
}

const createPMatrix = (currentMatrix: Array3x3, rowIndexOfMaxEl: number, rowIndexOfCurRow: number) => {
  const pMatrix: Array3x3 = [[],[],[]]

  for(let i = 0;  i < 3; i++) {
      pMatrix[i] = [0, 0, 0];
  }

  for (let i = 0; i < 3; i++) {
    if (i === rowIndexOfCurRow) {
      pMatrix[i][rowIndexOfMaxEl] = 1
    }
    else if (i === rowIndexOfMaxEl) {
      pMatrix[i][rowIndexOfCurRow] = 1
    }
    else pMatrix[i][i] = 1
  }
  return pMatrix
}

export { pStep }
