export const f = (x: number) => { return x*x*x*x*x + 2*(x**4) +3*(x**2); }

const eps = Math.pow(10, -3)

export const run = (a: number, b: number) => {
  let nByTwo = 2
 // let prevCalc = calculateForN(nByTwo, a, b)
//  let currCalc = calculateForN(nByTwo *= 2, a, b)
  console.log(calculateForN(4, a, b))
  // Runge's rule
//  while ((1/15) * Math.abs(currCalc - prevCalc) > eps) {
 //   prevCalc = currCalc
//    currCalc = calculateForN(nByTwo *= 2, a, b)
//  }
//  return currCalc
}

const calculateForN = (nByTwo: number, a: number, b: number) => {
  let result = 0

  const h = (b - a) / nByTwo;
  const vertices = []
  let currentStep = a
  while (currentStep <= b) {
    vertices.push(currentStep)
    currentStep += h
  }
  console.log(`adding f(first vertex) and f(last vertex): ${f(vertices[0])}, ${f(vertices[vertices.length - 1])}`)
  result = f(vertices[0]) + f(vertices[vertices.length - 1])
  console.log ('result', result)
  for (let i = 1; i < vertices.length - 1; i++) {
    if (i % 2 === 0)
    {
      console.log(`adding f(vertex ${i}) by 2: ${f(vertices[i]) * 2}`)
      result += f(vertices[i]) * 2
      console.log('result', result)
    }
    else {
      console.log(`adding f(vertex ${i}) by 4: ${f(vertices[i]) * 4}`)
      result += f(vertices[i]) * 4
      console.log('result', result)
    }

  }
  console.log('step length', h)
  console.log('returning result * step /3', result * h / 3)
  return result * h / 3
}
