namespace tests

open Xunit
open FsUnit

type ``Hello World tests`` ()=
    [<Fact>] member test.
     ``Should always succeed`` ()=
        Choice<int,int>.Choice1Of2(1) |> should be (choice 1)
