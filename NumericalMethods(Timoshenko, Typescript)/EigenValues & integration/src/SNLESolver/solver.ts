// page 55 1 zh
// page 66 2 a
const eps = Math.pow(10, -3)

export const solver = () => {
  let prevX = 0
  let prevY = 0
  let prevZ = 0

  let curX = 1.25
  let curY = 0
  let curZ = 0.25
  do {
    prevX = curX
    prevY = curY
    prevZ = curZ

    curX = Math.sqrt((-(1.5 * prevY * prevY) - (prevZ * prevZ) + 5) / 3)
    curY = (-6 / 5 * prevX * prevY * prevZ) + (1 / 5 * prevX) - (3 / 5 * prevZ)
    curZ = 1 / (5 * prevX - prevY)


    console.log('X',prevX, curX)
    console.log('Y',prevY, curY)
    console.log('Z',prevZ, curZ )
    console.log('___________')
  } while (!compare([prevX, curX], [prevY, curY], [prevZ, curZ]))

  return [curX, curY, curZ]
}

const compare = (x: [number, number], y: [number, number], z: [number, number]) => {
  if (Math.abs(x[0] - x[1]) < eps && Math.abs(y[0] - y[1]) < eps && Math.abs(z[0] - z[1]) < eps)
    return true
  return false
}

