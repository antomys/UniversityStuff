type Array3 = [number, number, number] | []
type Array3x3 = [Array3, Array3, Array3]

const transposeMatrix = (matrix: Array3x3) => {
  const transposedMatrix: Array3x3 = [[], [], []]

  for (let i = 0; i < 3; i++) {
    transposedMatrix[i] = [0, 0, 0]
  }

  for (let i = 0; i < 3; i++) {
    for (let j = 0; j < 3; j++) {
      transposedMatrix[j][i] = matrix[i][j]
    }
  }

  return transposedMatrix
}


const printMatrix3x3 = (matrix: Array3x3) => {
  console.log('_______________')
  for (let i = 0; i < 3; i++) {
    for (let j = 0; j < 3; j++) {
      process.stdout.write((matrix[i][j].toFixed(2)).padEnd(10))
    }
    console.log()
  }
}



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

export const solver = () => {
  let A: Array3x3 = [
    [1, 2, 3],
    [2, 3, 4],
    [3, 4, 5]]


 console.log('t(a)', square(A))
  for (let q = 0; q < 3; q++) {
    const [iMax, jMax, max]: number[] = findMax(A)
    console.log(`max, i, j: ${max}, ${iMax + 1}, ${jMax + 1}`)
    const tgTwoPhi = (A[iMax][iMax] - A[jMax][jMax])
      ? (2 * max) / (A[iMax][iMax] - A[jMax][jMax])
      : Math.PI / 4
    console.log(`tg2phi: ${tgTwoPhi}`)
    const phi = Math.abs(Math.atan(tgTwoPhi) / 2)
    console.log('phi', phi)
    const cosPhi = Math.cos(phi)
    const sinPhi = Math.sin(phi)
    console.log(`cosphi, sinphi: ${cosPhi}, ${sinPhi}`)
    const U: Array3x3 = [
      [0, 0, 0],
      [0, 0, 0],
      [0, 0, 0]
    ]
    for (let i = 0; i < 3; i++) {
      for (let j = 0; j < 3; j++) {
        U[i][j] = 0
        if (i === j) {
          U[i][j] = 1
        }
      }
    }
    U[iMax][iMax] = cosPhi
    U[iMax][jMax] = sinPhi
    U[jMax][iMax] = -sinPhi
    U[jMax][jMax] = cosPhi
    console.log('U matrix:')
    printMatrix3x3(U)
    const U_T = transposeMatrix(U)
    console.log('U_T:')
    printMatrix3x3(U_T)
    const an = multiplyMatrices(U, A)
    printMatrix3x3(an)
    const ANext = multiplyMatrices(an, U_T)
    console.log('A_Next:')
    printMatrix3x3(ANext)

    A = ANext

    console.log('t(a)', square(A))
  }

}

const findMax = (A: Array3x3) => {
  let iMax: number = 0, jMax: number = 0, max: number = -10000
  for (let i = 0; i < 3; i++) {
    for (let j = 0; j < 3; j++) {
      if (i !== j && max < Math.abs(A[i][j])) {
        iMax = i
        jMax = j
        max = Math.abs(A[i][j])
      }
    }
  }
  return [iMax, jMax, max]
}

const square = (A: Array3x3) => {
  let sum: number = 0
  for (let i = 0; i < 3; i++) {
    for (let j = 0; j < 3; j++) {
      if (i !== j) {
        sum += A[i][j] **2
      }
    }
  }
  return sum
}