namespace Library

open Avalonia.Controls.Shapes

module GOL =
    let neighborsOf (x, y) =
        let dx = [|-1; 0; 1; -1; 1; -1; 0; 1|]

        let dy = [|-1; -1; -1; 0; 0; 1; 1; 1|]

        let neighbors = List.map (fun i -> (x + dx.[i], y + dy.[i])) [0..(dx.Length - 1)]

        neighbors

    // AssertEquality [(-1,-1);(-0,-1);(1,-1);(-1,0);(1,0);(-1,1);(0,1);(1,1)] (neighborsOf (0,0)) 

    let livingNeighborsOf (x, y) living =
        let neighbors = neighborsOf (x,y)

        let livingNeighbors = List.filter (fun cell -> List.contains cell living) neighbors

        livingNeighbors.Length

    // AssertEquality 2 (livingNeighborsOf (0,0) [(-1,-1); (0,0); (1,0); (2,2)])

    let willLive (x,y) living =
        let livingNeighborsCount = livingNeighborsOf (x,y) living
        let willLiveAccordingToGOL = ((List.contains (x,y) living) && (livingNeighborsCount > 1) && (livingNeighborsCount < 4))
                                            ||
                                            (not (List.contains (x,y) living) && (livingNeighborsCount = 3))
        willLiveAccordingToGOL

    // AssertEquality false (willLive (0,0) [])
    // AssertEquality true (willLive (0,0) [(1,0); (0,0); (0,1)])
    // AssertEquality true (willLive (0,0) [(1,0); (-1,0); (0,1)])
    // AssertEquality false (willLive (0,0) [(1,0); (-1,0); (0,1); (0,-1)])

    // (defn next-generation [living]
    //   (into #{} (filter (fn [cell] (will-live? cell living)) (distinct (apply concat (map neighbors-of living))))))
    let nextGenerationOf living = 
        let potentialLiving = List.distinct (List.concat (List.map neighborsOf living))

        let nextGeneration = Set (List.filter (fun cell -> willLive cell living) potentialLiving)

        nextGeneration
    // let testNextGen = nextGenerationOf [(1,0); (0,0); (-1,0)]
    // AssertEquality [(0,-1); (0,0); (0,1)] testNextGen

    // Rectangle.create [
    //     Rectangle.fill "blue"
    //     Rectangle.width 63.0
    //     Rectangle.height 41.0
    //     Rectangle.left 40.0
    //     Rectangle.top 31.0
    // ]
    //return a list of Rectangles according to living cells
    let boardFromLiving living =
        //get total board size in integer
        //scale width/height according to canvas size and total board size
        //transfer each cell into a left/top location
        //create a rectangle at that location with correct size
        ()