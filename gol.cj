;; Game of life rules
;; 1) A living cell with 2 > neighbors > 3 dies
;; 2) A dead cell with 3 neighbors comes to life
;; 3) Any other cell remains in its current state

(ns game_of_life
  (:require [clojure.string :as string]
            [clojure.test :refer :all]
            [quil.core :as q]
            [quil.middleware :as m]))

;; (range 8) gives the values for [i]
(defn neighbors-of [[x y]]
  (let [dx [-1 0 1 -1 1 -1 0 1]
        dy [-1 -1 -1 0 0 1 1 1]]
    (into #{} (map (fn [i] [(+ x (dx i)) (+ y (dy i))]) 
                   (range 8)))))

(defn living-neighbors
  ([cell living]
   (let [x (first cell), y (second cell)]
     (count (filter living (neighbors-of [x y])))))
  ([living]
   (fn [cell] (living-neighbors cell living))))

(defn will-live? [[x y] living]
  (let [neighbors (living-neighbors [x y] living)]
    (if (or
         (and (living [x y]) (< 1 neighbors) (> 4 neighbors))
         (and (not= (living [x y]) true) (= 3 neighbors)))
      true false)))

(defn next-generation [living]
  (into #{} (filter (fn [cell] (will-live? cell living)) (distinct (apply concat (map neighbors-of living))))))





;; gui

(defn setup []
  ; initial state
  (q/frame-rate 30)
  (into #{}
        (filter identity
                (for [x (range 1 21)
                      y (range 1 21)]
                  (if (= 0 (rand-int 5)) [x y] nil)))))
  
(defn draw [state]
  (q/background 255)
  (q/fill 34 95 215)

  (doseq [[x y] state]
    (let [length 20]
      (q/rect (* x length) (* y length) length length))))

(defn run2 [opts]
  (q/defsketch GameOfLife
    :title "Conway's Game Of Life"
    :size [900 900]
    :setup setup
    :draw draw
    :update next-generation
    :middleware [m/fun-mode]))