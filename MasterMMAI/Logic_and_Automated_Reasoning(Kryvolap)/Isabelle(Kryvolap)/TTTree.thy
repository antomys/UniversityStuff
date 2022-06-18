theory TTTree
imports Main "HOL-Data_Structures.Set_Specs"

begin

nitpick_params[verbose, timeout = 1000]

(*Necessary Datatypes*)

datatype 'a tttree = Empty | SmallNode "'a tttree" 'a  "'a tttree" |
     LargeNode "'a tttree"  'a  "'a tttree"  'a "'a tttree"
                                                           
datatype 'a TempDel =  Hole "'a tttree" |Nor "'a tttree"

datatype 'a TempIns =  Norm "'a tttree"  | KickUp "'a tttree" 'a "'a tttree"

datatype Eval = Smaller | Larger | Bet

datatype 'a May = None | Option 'a

datatype Emp = Em | Na


(*Lookup function*)

primrec contains :: "('a::linorder) \<Rightarrow> 'a tttree \<Rightarrow> bool" where    
"contains a Empty = False" |
"contains a (SmallNode l v r)  = 
        (if a < v then contains a l
        else if a > v then contains a r 
        else True)" |
"contains a (LargeNode l lv m rv r) =
        (if a = lv  then True
        else if a = rv then True
        else if a < lv then contains a l
        else if a > rv then contains a r 
        else contains a m)"  

(*Helper Functions*)

definition eval :: "('a::linorder) \<Rightarrow> ('a::linorder) \<Rightarrow> 'a May  \<Rightarrow> Eval" where
"eval a v t  = (case t of 
                None \<Rightarrow> (if a <v then Smaller
            else if a > v then Larger
            else Bet)|
                (Option l) \<Rightarrow> if (a <v) then Smaller
            else if a > l then Larger
            else Bet)"


primrec elements :: "'a tttree \<Rightarrow> 'a list" where 
"elements Empty = []"|
"elements (SmallNode l v r) = (elements l) @ [v] @ (elements r)"|
"elements (LargeNode l lv m rv r) = (elements l) @ [lv] @ (elements m) @ [rv] @ (elements r)"

definition empty :: "'a tttree  \<Rightarrow> Emp"  where
"empty a  = (if (a = Empty) then Em
             else Na)"

(*Insert and aux functions*)


fun insert:: "('a::linorder) \<Rightarrow> 'a tttree \<Rightarrow> 'a TempIns" where
"insert a Empty = (KickUp (Empty) a (Empty))"|
"insert a (SmallNode l v r) =  (case eval a v None of 
                   Smaller \<Rightarrow> (case insert a l of
                              (Norm a) \<Rightarrow> (Norm (SmallNode a v r))|
                              (KickUp ll vv rr) \<Rightarrow> (Norm(LargeNode ll vv rr v r )))|
                   Larger \<Rightarrow> (case insert a  r of
                              (Norm a) \<Rightarrow> (Norm (SmallNode l v a))|
                              (KickUp ll vv rr) \<Rightarrow> (Norm(LargeNode l v ll vv rr )))|
                   Bet \<Rightarrow> (Norm ((SmallNode l v r)))) "|
"insert a (LargeNode l lv m rv r) = (case eval a lv (Option rv) of 
             Smaller \<Rightarrow> (case insert a l of
                        (Norm a) \<Rightarrow> (Norm (LargeNode a lv m rv r))|
                        (KickUp ll vv rr) \<Rightarrow> (KickUp (SmallNode ll vv rr) lv (SmallNode m rv r)))|
             Larger \<Rightarrow> (case insert a r of 
                       (Norm a) \<Rightarrow> (Norm (LargeNode l lv m rv a))|
                       (KickUp ll vv rr) \<Rightarrow> (KickUp (SmallNode l lv m) rv (SmallNode ll vv rr)))|
             Bet \<Rightarrow> (case insert a m of 
                       (Norm a) \<Rightarrow> (Norm (LargeNode l lv a rv r))|
                       (KickUp ll vv rr) \<Rightarrow> (KickUp (SmallNode l lv ll) vv (SmallNode rr rv r))))" 

definition convertKick :: "'a TempIns \<Rightarrow> 'a tttree " where 
"convertKick k = (case k of
    (Norm a) \<Rightarrow> a|
    (KickUp ll vv rr) \<Rightarrow> (SmallNode ll vv rr))"


(*Checks if the value to be inserted is already present in the tree,
 if it is skip the insertion*)
fun ins :: "('a::linorder) \<Rightarrow> 'a tttree \<Rightarrow> 'a tttree" where 
"ins a t =( case (contains a t) of
           True \<Rightarrow> t|
           False \<Rightarrow> (convertKick(TTTree.insert a t)))" 


(* ========================================== Delete and aux functions =========================================*)


(*Checks if the value to delete is in the current node*)

primrec nxt :: "'a list \<Rightarrow> ('a::linorder) \<Rightarrow> 'a May" where
"nxt [] a = None"|
"nxt (x#xs) a = (if x > a then (Option x) 
               else (nxt xs a))"


primrec replace :: "('a::linorder) \<Rightarrow> ('a::linorder) \<Rightarrow> 'a tttree \<Rightarrow> 'a tttree" where
"replace _ _ Empty = Empty"|
"replace old new (SmallNode l v r) = (case old = v of 
                                     True \<Rightarrow> (SmallNode l new r)|
                                     False \<Rightarrow> (if old < v then (SmallNode (replace old new l) v r)
                                              else (SmallNode l v (replace old new r))))"|
"replace old new (LargeNode l lv m rv r) = (case old = lv of 
                                     True \<Rightarrow> (LargeNode l new m rv r)|
                                     False \<Rightarrow> (case old = rv of 
                                                  True \<Rightarrow>  (LargeNode l new m rv r)|
                                                  False \<Rightarrow> (if old < lv then (LargeNode (replace old new l) lv m rv r)
                                                            else if old > rv then  (LargeNode l lv m rv (replace old new r ))
                                                            else (LargeNode l lv (replace old new m) rv r))))"



(*Delete successor, then replace the value to be deleted from the result of that with the successor *)

fun small_large :: "'a tttree \<Rightarrow> 'a tttree \<Rightarrow> 'a  \<Rightarrow> 'a TempDel" where
" small_large Empty _ _ = Hole Empty"|
" small_large (SmallNode  ll vv rr) c v = (Hole (LargeNode ll vv rr v c ))"|
" small_large (LargeNode   lll llv mm rrv rrr) c v = (Nor (SmallNode (SmallNode lll llv mm) rrv (SmallNode rrr v c)))"

fun small_small :: "'a tttree \<Rightarrow> 'a tttree \<Rightarrow> 'a \<Rightarrow> 'a TempDel" where
 "small_small Empty _ _ = (Hole Empty)"|
 " small_small (SmallNode  ll vv rr) c v = (Hole (LargeNode c v ll vv rr ))"|
 "small_small (LargeNode lll llv mm rrv rrr) c v = (Nor (SmallNode (SmallNode c v lll) llv (SmallNode mm rrv rrr)))"

primrec large_large :: "'a tttree \<Rightarrow>'a tttree \<Rightarrow> 'a tttree \<Rightarrow> 'a \<Rightarrow> 'a \<Rightarrow> 'a TempDel" where 
" large_large Empty _ _ _ _= (Hole Empty)"|
"large_large (SmallNode  ll v rr) c l lv rv = (Nor(SmallNode l lv (LargeNode ll v rr rv c)))"|
" large_large (LargeNode ll llv mm rrv rr) c l lv rv = (Nor (LargeNode l lv (SmallNode ll llv mm) rrv (SmallNode rr rv c) ))"


primrec large_small :: " 'a tttree \<Rightarrow> 'a tttree \<Rightarrow> 'a tttree \<Rightarrow> 'a \<Rightarrow> 'a \<Rightarrow> 'a TempDel" where
"large_small Empty _ _ _ _ = (Hole Empty)"|
"large_small(SmallNode ll v rr) c r lv rv =  Nor(SmallNode (LargeNode c lv ll v rr) rv r)"|
"large_small (LargeNode ll llv mm rrv rr) c r lv rv = Nor (LargeNode (SmallNode c lv ll) llv (SmallNode mm rrv rr) rv r)"

primrec large_mid ::  " 'a tttree \<Rightarrow> 'a tttree \<Rightarrow> 'a tttree \<Rightarrow> 'a \<Rightarrow> 'a \<Rightarrow> 'a TempDel" where
"large_mid Empty _ _ _ _ = (Hole Empty)"|
"large_mid (SmallNode  ll v rr) c r lv rv  = Nor (SmallNode (LargeNode ll v rr lv c) rv r)"|
"large_mid (LargeNode ll llv mm rrv rr) c r lv rv = Nor (LargeNode (SmallNode ll llv mm) rrv (SmallNode rr lv c) rv r)"

(*TODO : Add removal of elements from non-terminal nodes*)
fun delete:: "('a::linorder) \<Rightarrow> 'a tttree \<Rightarrow> 'a TempDel" where
"delete a Empty = Hole Empty"|
"delete a (SmallNode l v r) =  (case (eval a v None) of
                             Larger \<Rightarrow> (case (delete a r) of
                                       (Nor c) \<Rightarrow> Nor (SmallNode l v c)|
                                       (Hole c) \<Rightarrow> (small_large l c v))  |
                             Smaller \<Rightarrow> (case (delete a l) of
                                       (Nor b) \<Rightarrow> Nor (SmallNode b v r)|
                                       (Hole c) \<Rightarrow>(small_small r c v)) |
                             Bet \<Rightarrow> Nor (SmallNode l v r))"|
"delete a (LargeNode l lv m rv r) = (case (empty l) of
          Em \<Rightarrow> (if a = lv then (Nor(SmallNode Empty rv Empty))
                   else (Nor(SmallNode Empty lv Empty)))|
          Na \<Rightarrow> (case (eval a lv (Option rv)) of
                            Larger \<Rightarrow> (case (delete a r) of
                                      (Nor c) \<Rightarrow> Nor (LargeNode l lv m rv c)|
                                      (Hole c) \<Rightarrow> (large_large m c l lv rv ))|
                            Smaller \<Rightarrow> (case (delete a l) of
                                       (Nor c) \<Rightarrow> Nor (LargeNode c lv m rv r)|
                                       (Hole c) \<Rightarrow> large_small m c r lv rv)|
                            Bet \<Rightarrow> (case (delete a m) of
                                   (Nor c) \<Rightarrow> Nor (LargeNode l lv c rv r)|
                                   (Hole c) \<Rightarrow> large_mid l c r lv rv)))"


definition convertHole:: "'a TempDel \<Rightarrow> 'a tttree" where 
"convertHole a = (case a of
                 (Nor b) \<Rightarrow> b|
                 (Hole b) \<Rightarrow> b)"


primrec leaf :: "('a::linorder) \<Rightarrow> 'a tttree \<Rightarrow> bool" where    
"leaf a Empty = False" |
"leaf a (SmallNode l v r)  = 
        (if a < v then leaf a l
        else if a > v then leaf a r 
        else (if l = Empty then True else False))" |
"leaf a (LargeNode l lv m rv r) =
        (if a = lv  then (if l = Empty then True else False)
        else if a = rv then (if r = Empty then True else False)
        else if a < lv then leaf a l
        else if a > rv then leaf a r 
        else leaf a m)"  

definition del :: "('a::linorder) \<Rightarrow> 'a tttree \<Rightarrow> 'a tttree" where
"del a t = (case contains a t of
                False \<Rightarrow> t|
                True \<Rightarrow>(case leaf a t of
                             True \<Rightarrow> convertHole (delete a t)|
                             False \<Rightarrow>(case (nxt (elements t) a) of
                                     None \<Rightarrow> t|
                                     Option b \<Rightarrow>replace a b (convertHole(delete b t)))))"                             


(*==========================================Auxilliary functions for proving========================================================*)

fun heightTree :: "'a tttree \<Rightarrow> nat" where
"heightTree Empty = 0"|
"heightTree (SmallNode l v r) = (max (heightTree l) (heightTree r)) + 1"|
"heightTree (LargeNode l lv m rv r) = (max (heightTree l) (max(heightTree m) (heightTree r))) + 1 "

primrec TT :: "'a tttree \<Rightarrow> bool" where 
"TT Empty = True"|
"TT (SmallNode l _ r) = ((heightTree l = heightTree r) & (TT l) & (TT r) )"|
"TT (LargeNode l _ m _ r) = (((heightTree l  = heightTree r) \<and> (heightTree l = heightTree m)) \<and> (TT l) \<and> (TT m) \<and> (TT r))"

fun kickHeight:: "'a TempIns \<Rightarrow> nat" where 
"kickHeight (Norm a) = heightTree a"|
"kickHeight (KickUp l _ r) = (max (heightTree l) (heightTree r))"



(*============================================================PROOFS===============================================================*)
(*============================================================Correctness of contains================================================*)

theorem cont_corr_set : "sorted(elements t) \<Longrightarrow> contains a t = (a \<in> set (elements t))"
proof (induct t)
  case Empty 
  then show ?case by simp
next
  case (SmallNode l v r)
  then show ?case by (simp add: isin_simps) (auto)
next
  case (LargeNode l lv m rv r)
  then show ?case by (simp add: isin_simps) (meson UnCI not_less_iff_gr_or_eq)
qed 
(* apply(induct t)
   apply(simp)
   apply(simp_all add:isin_simps)
   apply auto
   by (meson UnCI not_less_iff_gr_or_eq) *)



(*===============================================================Insertion proofs order====================================================*)


theorem ins_order : "\<lbrakk>a \<notin> set( elements t) ;sorted (elements t); distinct (elements t); (TT t) = True\<rbrakk> \<Longrightarrow>  elements (convertKick (insert a t)) =  ins_list a (elements t)"
proof (induct t)
  case Empty 
  then show ?case by (simp add: convertKick_def)
next 
  case (SmallNode l v r) 
  then show ?case by (simp add: convertKick_def ins_list_simps sorted_Cons_iff eval_def split: TempIns.splits)
next 
  case (LargeNode l lv m rv r)
  then show ?case by (simp add: convertKick_def ins_list_simps sorted_Cons_iff eval_def split: TempIns.splits)
qed 
(*  apply(induct t)
  apply(simp_all add:  ins_list_simps TTTree.eval_def sorted_Cons_iff  convertKick_def split: TempIns.splits )
  done *)


lemma ins_list_same : "\<lbrakk> a \<in> set l;sorted l\<rbrakk> \<Longrightarrow> ins_list a l = l"
  apply(induction l)
  apply(simp_all)
  using less_imp_not_less sorted_Cons_iff by blast 



(*Necessary ??*)
theorem insert_order : "\<lbrakk>sorted (elements t);distinct (elements t) ;(TT t) = True\<rbrakk> \<Longrightarrow>  elements(ins a t) =  ins_list a (elements t)"
  apply(simp split: bool.splits)
  apply(simp add: cont_corr_set ins_list_same)
  apply(simp add: ins_order cont_corr_set)
  done 

(*===============================================================Insertion proofs full====================================================*)
lemma complete_ins : "\<lbrakk>a \<notin> set( elements t) ;TT t\<rbrakk> \<Longrightarrow> (TT (convertKick (insert a t))) \<and> heightTree t = kickHeight (insert a t)"
proof (induct t)
  case Empty 
  then show ?case by (simp add:convertKick_def)
next
  case (SmallNode l _ r) 
  then show ?case by (auto split:  TempIns.splits May.splits Eval.splits) (simp_all add: convertKick_def)
next
  case (LargeNode l _ m _ r)
  then show ?case by (auto split:  TempIns.splits May.splits Eval.splits) (simp_all add: convertKick_def)
qed
(* 
  apply(induct t)
  apply (auto split!: TempIns.splits May.splits Eval.splits)
  apply(simp_all add: convertKick_def )
  done*)


lemma complete_insert : "\<lbrakk>sorted(elements t); TT t\<rbrakk> \<Longrightarrow> TT (ins a t) "
  apply(simp split: bool.splits)
  apply(simp_all add:convertKick_def)
  by (metis complete_ins cont_corr_set convertKick_def)




(*===============================================================Deletion proofs order====================================================*)

lemma empty_height : "(empty t = Em) = (heightTree t = 0)"
  apply(induct t)
  apply(simp_all add: empty_def)
  done


lemma small_large_ord : "\<lbrakk>l \<noteq> Empty\<rbrakk> \<Longrightarrow>  (elements (convertHole (small_large l r val)) =  elements(l) @ val # elements (r) )"
  apply(induct l)
   apply(auto simp: convertHole_def)
  done

lemma small_small_ord : "\<lbrakk>l \<noteq> Empty\<rbrakk> \<Longrightarrow>  (elements (convertHole (small_small l r val)) =  elements(r) @ val # elements (l) ) "
  apply(induct l)
   apply(auto simp: convertHole_def)
  done 

lemma large_large_ord : "\<lbrakk>t1 \<noteq> Empty\<rbrakk> \<Longrightarrow>  elements (convertHole (large_large t1 t2 t3  val1 val2)) =  elements(t3) @ val1 # elements (t1) @val2 # elements t2"
  apply(induct t1)
   apply(auto simp: convertHole_def)
  done

lemma large_small_ord : "\<lbrakk>t1 \<noteq> Empty\<rbrakk> \<Longrightarrow>  elements (convertHole (large_small t1 t2 t3  val1 val2)) =  elements(t2) @ val1 # elements (t1) @val2 # elements t3"
  apply(induct t1)
   apply(auto simp: convertHole_def)
  done 
  

lemma large_mid_ord : "\<lbrakk>t1 \<noteq> Empty\<rbrakk> \<Longrightarrow>  elements (convertHole (large_mid t1 t2 t3  val1 val2)) =  elements(t1) @ val1 # elements (t2) @val2 # elements t3"
  apply(induct t1)
   apply(auto simp: convertHole_def)
  done 

thm sorted_snoc_iff

theorem delete_ord : "\<lbrakk>sorted (elements t); leaf a t;distinct(elements(t)); TT t \<rbrakk> \<Longrightarrow> (elements(convertHole (delete a t))) = (del_list a (elements t))"
  apply(induct t)
  apply( simp_all split!: TempDel.splits Eval.splits Emp.splits)
  apply( simp_all add: sorted_Cons_iff del_list_simps sorted_snoc_iff convertHole_def )
  apply(simp_all add: empty_height)
  apply auto[1]
  sorry


lemma del_ord : "\<lbrakk>sorted (elements t);distinct(elements(t)); (TT t) = True \<rbrakk> \<Longrightarrow> (elements(del a t)) = (del_list a (elements t))"
  apply(simp add: del_def)
  apply(auto split : bool.splits)
     apply(simp add: delete_ord)
    apply(simp_all add: cont_corr_set)
    apply (simp_all add: del_list_idem)
  apply(simp split: May.splits)
  apply auto
  oops 


(*==========================================Join========================================================================*)

fun join_aux :: "('a::linorder) list \<Rightarrow> ('a :: linorder) tttree \<Rightarrow> 'a tttree " where
"join_aux [] t = t"|
"join_aux (a#b) t = join_aux b (ins a t)"

definition join :: "('a::linorder) tttree \<Rightarrow> ('a::linorder)tttree \<Rightarrow> 'a tttree" where
"join a b =  join_aux (elements a) b "

(*Reformulate lemma in correct way*)
lemma join_aux_ord : " \<lbrakk>sorted l ;sorted(elements t)\<rbrakk> \<Longrightarrow> elements (join_aux l t ) = l@(elements t)"
  apply(induct l)
  apply auto  
  apply(simp split: bool.split)
  apply rule
  apply rule
  apply (simp_all add: sorted_Cons_iff)
   apply auto
   defer
  sorry
  

theorem join_ord : "\<lbrakk>TT t1;distinct (elements (t2)) ;TT t2; sorted (elements t1);  sorted (elements t2) \<rbrakk> \<Longrightarrow>sorted (elements( join t1 t2)) "
  apply(simp add: join_def)
  apply(simp add: join_aux_ord)

  oops
  
export_code contains empty insert convertKick ins  delete convertHole leaf replace  del in Haskell module_name TTTree

end
