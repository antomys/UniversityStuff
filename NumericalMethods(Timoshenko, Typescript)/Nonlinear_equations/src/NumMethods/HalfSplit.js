const evaluate = (x) => {
  return Math.pow(x, 3) - 4 * Math.pow(x, 2) - 4*x + 16
}

const getIterationCount = (a, b, eps) => {
  return Math.trunc(Math.log2((b - a) / eps))
}

const getSign = (x) => {
  return x > 0 ? "plus" : "minus";
}

export const halfSplit = (lo, hi, eps) => {

  let mid;
  let i = 1;
  let resultArray = [];
  const iterationCount = getIterationCount(lo, hi, eps)
 // console.log(iterationCount, eps)
  while (i <= iterationCount && evaluate(mid) !== 0) {
    mid = (lo + hi) / 2;
   // console.log (lo, mid, hi, evaluate(mid))
    if (getSign(evaluate(mid)) === getSign(evaluate(lo))) {
      lo = mid
    }
    else if (getSign(evaluate(mid)) === getSign(evaluate(hi))) {
      hi = mid
    }
    resultArray.push({iteration: i, x: mid, value: evaluate(mid)})
    i++
  }
  return resultArray
}
