type Array3 = [number, number, number] | []
type Array3x3 = [Array3, Array3, Array3]


const multiplyMatrices = (firstMatrix: Array3x3, secondMatrix: Array3x3) => {
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

const printMatrix3x3 = (matrix: Array3x3) =>
{
  console.log('_______________')
  for (let i = 0; i < 3; i++) {
    for (let j = 0; j < 3; j++) {
      process.stdout.write((matrix[i][j].toFixed(2)).padEnd(10))
    }
    console.log(  )
  }
}

const printVector = (vector: Array3) =>
{
  console.log('_______________')
  for (let i = 0; i < 3; i++) {
    console.log(vector[i].toFixed(2) + " ")
  }
}

const backwardsSolvingTop = (matrix: Array3x3, vector: Array3) =>
{
  const solution: Array3 = [0, 0, 0]
  console.log('matrix berfoe backwards TOp')
  printMatrix3x3(matrix)
  printVector(vector)

  for (let i = 0; i < 3; i++) {
    const rowCoefficient = matrix[i][i]
    for (let j = 0; j < 3; j++) {
      matrix[i][j] /= rowCoefficient
    }
    vector[i] /= rowCoefficient
  }

  printMatrix3x3(matrix)


  for (let i = 0; i < 3; i++) {
    let subtractionPart = 0
    for (let j = 0; j < i; j++) {
      subtractionPart += matrix[i][j] * solution[j]
    }
    solution[i] = vector[i] - subtractionPart
  }
  printVector(solution)
  return solution
}


const backwardsSolvingBottom = (matrix: Array3x3, vector: Array3) =>
{
  const solution: Array3 = [0, 0, 0]

  printMatrix3x3(matrix)
  printVector(vector)
  for (let i = 0; i < 3; i++) {
    const rowCoefficient = matrix[i][i]
    for (let j = 0; j < 3; j++) {
      matrix[i][j] /= rowCoefficient
    }
    vector[i] /= rowCoefficient
  }
  printMatrix3x3(matrix)
  printVector(vector)


  for (let i = 2; i >= 0; i--) {
    let subtractionPart = 0
    for (let j = i; j < 3; j++) {
      subtractionPart += matrix[i][j] * solution[j]
    }
    solution[i] = vector[i] - subtractionPart

  }

  return solution
}

const createSDMatrices = (currentMatrix: Array3x3) => {
  const dMatrix : Array3x3 = [[],[],[]]
  const sMatrix: Array3x3 = [[],[],[]]

  for (let i = 0; i < 3; i++) {
    dMatrix[i] = [0,0,0]
    sMatrix[i] = [0,0,0]
  }

  // fill in diagonal elements
  for (let i = 0; i <= 2; i++) {
    let subtractionPart = 0
    for (let p = 0; p <= (i - 1); p++) {
      subtractionPart += sMatrix[p][i] * sMatrix[p][i] * dMatrix [p][p]
    }
    dMatrix[i][i] = Math.sign(currentMatrix[i][i] - subtractionPart)
    sMatrix[i][i] = Math.sqrt(Math.abs(currentMatrix[i][i] - subtractionPart))
    for (let j = i + 1; j <= 2; j++) {
      if (i != 2)
      {
        subtractionPart = 0
        for (let p = 0; p <= (i - 1); p++) {
          subtractionPart += sMatrix[p][i] * dMatrix[p][p] * sMatrix[p][j]
        }
        console.log('sub part',subtractionPart,'notsub', currentMatrix[i][j])
        sMatrix[i][j] = (currentMatrix[i][j] - subtractionPart)/(dMatrix[i][i] * sMatrix[i][i])
      }
    }
  }



  return [dMatrix, sMatrix]
}

const transposeMatrix = (matrix: Array3x3) => {
  const transposedMatrix : Array3x3 = [[],[],[]]

  for (let i = 0; i < 3; i++) {
    transposedMatrix[i] = [0,0,0]
  }

  for (let i = 0; i < 3; i++) {
    for (let j = 0; j < 3; j++) {
      transposedMatrix[j][i] = matrix[i][j]
    }
  }

  return transposedMatrix
}

const solver = (currentMatrix: Array3x3, constantsVector: Array3) => {
  const [dMatrix, sMatrix] : Array3x3[] = createSDMatrices(currentMatrix)
  console.log('printing matrix D and matrix S:')
  printMatrix3x3(dMatrix)
  printMatrix3x3(sMatrix)
  const sTMatrix : Array3x3 = transposeMatrix(sMatrix)
  console.log('printing transposed matrix S:')
  printMatrix3x3(sTMatrix)
  console.log('printing multiplication of sT and D:')
  const sT_DMatrix = multiplyMatrices(sTMatrix, dMatrix)
  printMatrix3x3(sT_DMatrix)
  // normalize matrix -> solve first equation -> use the same method for second one
  console.log('printing top-bottom gauss solving of first equation:')
  const result : Array3 = backwardsSolvingTop(sT_DMatrix, constantsVector)
  console.log('printing vector y after first equation:')
  printVector(result)
  console.log('printing bottom-top gauss solving of second equation:')
  const finalResult : Array3 = backwardsSolvingBottom(sMatrix, result)
  console.log('RESULT')
  printVector(finalResult)

  printMatrix3x3(dMatrix)
  printMatrix3x3(sMatrix)
}

export {solver}