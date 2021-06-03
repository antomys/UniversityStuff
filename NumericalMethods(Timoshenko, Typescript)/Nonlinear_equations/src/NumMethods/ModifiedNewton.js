const evaluate = (x) => {
  return Math.pow(x, 3) - 7 * Math.pow(x, 2) + 7*x + 15 // x = -2 => f(x) = -35
}

const evaluateDerivative = (x) => {
  return 3 * Math.pow(x, 2) - 14 * x + 7 // x = -2 => f`(x) =12 + 28 + 7  = 47
}

const evaluateSecondDerivative = (x) => {
  return 6 * x - 14 // x = -2 => f``(x) = -26
}

export const modifiedNewton = (currentPoint) => {
  if (evaluate(currentPoint) * evaluateSecondDerivative(currentPoint) < 0) return [{iteration: 1, x: "NOT A VALID STARTING POINT", value: "HALTING!"}]
  const startingPoint = currentPoint
  let resultArray = []
  let i = 1
  // stop when we hit the precision threshold
  while (Math.abs (evaluate(currentPoint)) > Math.pow (10, -3)) {
    resultArray.push({iteration: evaluate(currentPoint), x: evaluateDerivative(startingPoint), value: evaluate(currentPoint) / evaluateDerivative(startingPoint)})
    currentPoint = currentPoint - (evaluate(currentPoint) / evaluateDerivative(startingPoint))
    resultArray.push({iteration: i, x: currentPoint, value: evaluate(currentPoint)})
    i++
  }
  return resultArray
}