type Array3 = [number, number, number] | []
type Array3x3 = [Array3, Array3, Array3]

const eps = Math.pow(10, -3)



const printMatrix3x3 = (matrix: Array3x3) => {
  console.log('_______________')
  for (let i = 0; i < 3; i++) {
    for (let j = 0; j < 3; j++) {
      process.stdout.write((matrix[i][j].toFixed(2)).padEnd(10))
    }
    console.log()
  }
}

export const solver = () => {
  let A: Array3x3 = [
    [2, 1, 0],
    [1, 2, 1],
    [0, 1, 2]
  ]
  let maxA = matrixNorm(A)
  console.log('matrix norm:', maxA)
  let B: Array3x3 = [[], [], []]
  for (let i = 0; i < 3; i++) {
    for (let j = 0; j < 3; j++) {
      if (i == j)
      {
        B[i][j] = maxA - A[i][j]
      }
      else {
        B[i][j] = (-1) * A[i][j]
      }
    }
  }

  console.log('B matrix:')
  printMatrix3x3(B)
  let maxB = maxEigen(B)
  console.log("_____________-")
  return maxA - maxB
}

const matrixNorm = (A: Array3x3) => {
  let max = -1
  for (let i = 0; i < 3; i++) {
    let cur = 0
    for (let j = 0; j < 3; j++) {
      cur += Math.abs(A[i][j])
    }
    if (max < cur) max = cur
  }
  return max
}

const maxEigen = (A: Array3x3) => {
  let currX: Array3 = [-1, -1, 0]
  let prevX: Array3 = [0, 0, 0]
  let currMu = 0
  let prevMu = 0
  do {
    console.log('__________-')
    let norm = vectorNorm(currX)
    console.log('vector norm:', norm)
    for (let i = 0; i < 3; i++) {
      prevX[i] = currX[i] / norm
    }

    console.log('normalized vector', prevX)
    currX = multiplyMatrixByVector(A, prevX)
    console.log('vector after multiplying by current matrix', currX)
    prevMu = currMu
    currMu = dotProduct(prevX, currX)
    console.log('currnet approximation:', currMu)
  } while (Math.abs(currMu - prevMu) > eps)
  return currMu
}


const dotProduct = (x: Array3, y: Array3) => {
  let res = 0
  for (let i = 0; i < 3; i++) {
    res += x[i] * y[i]
  }
  return res
}

const vectorNorm = (x: Array3) => {
  let res = 0
  for (let i = 0; i < 3; i++) {
    res += x[i] * x[i]
  }
  return Math.sqrt(res)
}

const multiplyMatrixByVector = (matrix: Array3x3, vector: Array3) => {
  const newVector: Array3 = new Array(3).fill(0) as Array3

  for (let i = 0; i < 3; i++) {
    for (let j = 0; j < 3; j++) {
      newVector[i] += matrix[i][j] * vector[j]
    }
  }
  return newVector
}