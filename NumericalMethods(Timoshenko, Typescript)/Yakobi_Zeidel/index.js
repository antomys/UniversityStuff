"use strict";


let dimensions = 3;
let matrix =
  [
    [-1, 3, 6],
    [3, -5, -4],
    [-6, -4, -4]
  ];

let b = [-9, 3, 4];
let result = [0, 0, 0];
let epsilon = 0.001;

let currentVector = [0, 0, 0];
let maxDelta = 0;
for (let q = 0; q <= 0; q++) {
  for (let i = 0; i < dimensions; i++) {
    // fixate element from solution vector
    currentVector[i] = b[i];
    // do subtractions
    for (let j = 0; j < dimensions; j++) {
      if (i != j)
        currentVector[i] -= matrix[i][j] * result[j];
    }
    // divide by major element
    currentVector[i] /= matrix[i][i]
  }
  maxDelta = -1;
  for (let i = 0; i < dimensions; i++) {
    const delta = Math.abs(result[i] - currentVector[i])
    if (delta > maxDelta)
      maxDelta = delta
    // push updated element back to result
    result[i] = currentVector[i]
  }
}

for (let i = 0; i < dimensions; i++){
  console.log(`Result[${i}] = ${currentVector[i]}`);
}


/*

let dimensions = 4;
let matrix = [
  [4, 0, 1, 0],
  [0, 3, 0, 2],
  [1, 0, 5, 1],
  [0, 2, 1, 4]];

let b = [12, 19, 27, 30];
let result = [0, 0, 0, 0];
let subtractionSum = 0;
let epsilon = 0.001;
let iterationDelta = 0

do {
  iterationDelta = 0;
  for (let i = 0; i < dimensions; i++) {
    const currentElement = result[i];
    subtractionSum = 0;
    // basic solving technique of lin. eq.
    for (let j = 0; j < dimensions; j++) {
      if (j != i)
        subtractionSum += matrix[i][j] * result[j];
    }
    result[i] = (b[i] - subtractionSum) / matrix[i][i];
    // find biggest delta
    let currentDelta = Math.abs(result[i] - currentElement)
    if (currentDelta > iterationDelta)
      iterationDelta = Math.abs(result[i] - currentElement);
  }
} while (iterationDelta > epsilon);

for (let i = 0; i < dimensions; i++) {
  console.log(`${result[i]}`);
}
*/

//# sourceMappingURL=index.js.map