const evaluate = (x) => {
    return Math.pow(x, 3) - 5 * Math.pow(x, 2)  - 4 * x + 20
}

export const TangentMethod = (lo, hi) => {
  let x0 = lo;
  let x1 = (lo + hi)/2
  let resultArray = []
  resultArray.push({iteration: 0, x: `Starting points: ${x0}, ${x1}`, value: "N/A"})
  let i = 1
  let x2
  while (Math.abs(evaluate(x1)) > Math.pow (10, -3)) {
    x2 = x1 - ((x1 - x0) * evaluate(x1)) / (evaluate(x1) - evaluate(x0))
    x0 = x1;
    x1 = x2
    resultArray.push({iteration: i, x: x2, value: evaluate(x2)})
    i++
  }
  return resultArray
}