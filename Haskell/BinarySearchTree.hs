data Tree a = EmptyTree | Node a (Tree a) (Tree a) deriving Show
data Status = HasRight | HasLeft | HasTwo | HasNot deriving (Eq)

singleton :: a -> Tree a
singleton x = Node x EmptyTree EmptyTree

treeInsert :: (Ord a) => a -> Tree a -> Tree a
treeInsert x EmptyTree = singleton x
treeInsert x (Node a left right)
  | x == a = Node x left right
  | x < a  = Node a (treeInsert x left) right
  | x > a  = Node a left (treeInsert x right)

treeSearch :: (Ord a) => a -> Tree a -> Bool
treeSearch _ EmptyTree = False
treeSearch x (Node a left right)
  | x == a = True
  | x < a  = treeSearch x left
  | x > a  = treeSearch x right

treeRemove :: (Ord a) => a -> Tree a -> Tree a
treeRemove _ EmptyTree = EmptyTree
treeRemove x (Node a left right)
  | x == a && status == HasTwo   = Node (treeMin right) left (treeRemove (treeMin right) right)
  | x == a && status == HasNot   = EmptyTree
  | x == a && status == HasLeft  = left
  | x == a && status == HasRight = right
  | x < a                        = Node a (treeRemove x left) right
  | x > a                        = Node a left (treeRemove x right)

  where
    status = nodeStatus left right

    nodeStatus :: Tree a -> Tree a -> Status
    nodeStatus EmptyTree EmptyTree = HasNot
    nodeStatus (Node _ _ _) (Node _ _ _) = HasTwo 
    nodeStatus (Node _ _ _) EmptyTree = HasLeft 
    nodeStatus EmptyTree (Node _ _ _) = HasRight

    treeMin :: (Ord a) => Tree a -> a
    treeMin (Node a EmptyTree EmptyTree) = a
    treeMin (Node _ left _) = treeMin left


testIntTree :: Tree Int
testIntTree = Node 5
            (Node 2 (Node 1 EmptyTree EmptyTree) (Node 4 (Node 3 EmptyTree EmptyTree) EmptyTree)) 
            (Node 6 EmptyTree (Node 7 EmptyTree EmptyTree))

testCharTree :: Tree Char
testCharTree = Node 'H'
            (Node 'B' (Node 'A' EmptyTree EmptyTree) (Node 'D' (Node 'C' EmptyTree EmptyTree) EmptyTree)) 
            (Node 'J' EmptyTree (Node 'R' EmptyTree EmptyTree))