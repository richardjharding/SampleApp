open canopy
open runner
open System



chromeDir <- "./"
start chrome

"Test Home Page of sample" &&& fun _ ->
    url "http://localhost:59714"

    contains "Sprydon Sample Application" (read ".navbar-header")

"Test About page" &&& fun _ ->
    url "http://localhost:59714/about"

    contains "About" (title ())


run()

printfn "press [enter] to exit"
System.Console.ReadLine() |> ignore

quit()



