import React from 'react'
import {halfSplit} from './NumMethods/HalfSplit'
import {modifiedNewton} from './NumMethods/ModifiedNewton'
import {TangentMethod} from './NumMethods/TangentMethod'

const App = () => {
  console.log ('____________________')
  return (
    <div>
      <p>Half-split method!</p>
      <p>Starting interval: (3, 10). Root located at point x = 4</p>
      <table>
        <tbody>
          <tr>
            <td>
              iteration
            </td>
            <td>
              X
            </td>
            <td>
              F(X)
            </td>
          </tr>
          {
            halfSplit(-3, 0, Math.pow(10, -3))
            .map(
              entry =>
              <tr key = {entry.x}>
                <td>
                  {entry.iteration}
                </td>
                <td>
                  {entry.x}
                </td>
                <td>
                  {entry.value}
                </td>
            </tr>
            )
          }
        </tbody>
      </table>
      <p>Modified Newton's method!</p>
      <p>Starting from: -2 </p>
      <p>Root is located at point x = -1.</p>
      <table>
        <tbody>
          <tr>
            <td>
              iteration
            </td>
            <td>
              X
            </td>
            <td>
              F(X)
            </td>
          </tr>
          {
            modifiedNewton(-2)
            .map(
              entry =>
              <tr key = {entry.x}>
                <td>
                  {entry.iteration}
                </td>
                <td>
                  {entry.x}
                </td>
                <td>
                  {entry.value}
                </td>
            </tr>
            )
          }
        </tbody>
      </table>

      <p>Tangent method!</p>
      <p>Root is located at point x = 5. Range (4.5, 7.5)</p>
      <table>
        <tbody>
          <tr>
            <td>
              iteration
            </td>
            <td>
              X
            </td>
            <td>
              F(X)
            </td>
          </tr>

          {
            TangentMethod(4.5, 7.5)
            .map(
              entry =>
              <tr key = {entry.x}>
                <td>
                  {entry.iteration}
                </td>
                <td>
                  {entry.x}
                </td>
                <td>
                  {entry.value}
                </td>
            </tr>
            )
          }
        </tbody>
      </table>
    </div>
  )
}

export default App