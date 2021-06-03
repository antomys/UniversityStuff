import {run as SimpsonRun} from './Simpson/simpson'
import {solver as SNLESolver} from './SNLESolver/solver'
import {solver as EigenValueSolver} from './Eigenvalues/solver'
import {solver as RotateYakobi} from './RotateYakobi/solver'

//console.log(SimpsonRun(0, 4))
//console.log(SNLESolver())
console.log(EigenValueSolver())
//console.log(RotateYakobi())