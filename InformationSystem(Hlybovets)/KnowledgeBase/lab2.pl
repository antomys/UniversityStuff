main :-
    introduction,
    reset_answers,
    find_pizza(Pizza),
    describe(Pizza), nl.
  
  introduction :-
    write('What kind of pizza do I want?'), nl,
    write('To answer, input the number shown next to each answer, followed by a dot (.)'), nl, nl.
  
  
  find_pizza(Pizza) :-
    pizza(Pizza), !.
  
  :- dynamic(progress/2).
  
  reset_answers :-
    retract(progress(_, _)),
    fail.
  reset_answers.
  
  % Rules
  pizza(margherita) :-
      why(hungry),
      smt_exotic(no),
      pepper(yes),
      hot(not_bad),
      meat(not_bad).
  
  pizza(meats) :-
      why(hungry),
      meat(im_a_fan),
      hot(love).
  
  pizza(calzone) :-
      smt_exotic(yes),
      pepper(yes),
      hot(not_bad),
      meat(love).
  
  pizza(veg) :-
      meat(hate),
      smt_sweet(no).
  
  pizza(hawaiian) :-
      why(for_fun),
      smt_exotic(yes),
      smt_sweet(yes),
      meat(love),
      pepper(yes).
  
  pizza(cheese_bomb) :-
      cheese(im_a_fan).
  
  pizza(diablo) :-
      why(for_fun),
      pepper(yes),
      hot(im_a_fan),
      meat(love).
  
  pizza(paradize) :-
      why(for_fun),
      smt_exotic(yes),
      smt_sweet(yes).
  
  % Questions
  question(fav_ing) :-
      write('What is your favorite ingredient?'), nl.
  
  question(why) :-
      write('Why do you want a pizza?'), nl.
  
  question(cheese) :-
      write('How much do you love cheese?'), nl.
  
  question(smt_sweet) :-
      write('Do you want smt sweet?'), nl.
  
  question(smt_exotic) :-
      write('Do you love smt exotic?'), nl.
  
  question(pepper) :-
      write('Do you love pepper?'), nl.
  
  question(hot) :-
      write('How much do you love smt hot?'), nl.
  
  question(meat) :-
      write('How much do you love meat?'), nl.
  
  % Answers
  answer(for_fun) :-
      write('For fun').
  
  answer(i_dont_know) :-
      write('I don\'t know').
  
  answer(hungry) :-
      write('I\'m hungry').
  
  answer(hate) :-
      write('Hate').
  
  answer(love) :-
      write('Love').
  
  answer(yes) :-
    write('Yes').
  
  answer(no) :-
      write('No').
  
  answer(im_a_fan) :-
    write('I\'m a fan!').
  
  answer(not_bad) :-
      write('Not Bad').
  
  answer(meat) :-
      write('Meat').
  
  answer(vegetables) :-
      write('Vegetables').
  
  answer(pepper) :-
      write('Pepper').
  
  answer(cheese) :-
      write('Cheese').
  
  % Descriptions
  describe(margherita) :-
    write('Pizza Margherita'), nl,
    write('The pizza Margherita is just over a century old and is named after HM Queen Margherita of Italy, wife of King Umberto I and first Queen of Italy. It\'s made using toppings of tomato, mozzarella cheese, and fresh basil, which represent the red, white, and green of the Italian flag.'), nl.
  
  describe(cheese_bomb) :-
    write('Pizza Cheese Bomb'), nl,
    write('Parmesan, Romano, Fontina, Gouda, Ricotta & Mozzarella Cheeses on an Alfredo Sauce'), nl.
  
  describe(calzone) :-
    write('Pizza Calzone'), nl,
    write('Calzone means \'stocking\' in Italian and is a turnover that originates from Italy. Shaped like a semicircle, the calzone is made of dough folded over and filled with the usual pizza ingredients.'), nl.
  
  describe(veg) :-
    write('Vegetarian pizza '), nl,
    write('Tomatoes, mushrooms, green peppers, onions, black olives on zesty red sauce.'), nl.
  
  describe(hawaiian) :-
    write('Hawaiian pizza '), nl,
    write('Tender ham & juicy pineapple on zesty red sauce.'), nl.
  
  describe(paradize) :-
    write('Paradize pizza '), nl,
    write('Ricotta,cherry,mint, strawberry.'), nl.
  
  describe(diablo) :-
    write('Diablo pizza '), nl,
    write('Onion, Red Pepper, Cherry Tomatoes, Broccoli & Mushrooms, Marinara, Mozzarella & Parmesan'), nl.
  
  describe(meats) :-
    write('The Ultimate in Premium Meats '), nl,
    write('Primo pepperoni, linguica, bacon, Italian sausage on zesty red sauce. '), nl.
  
  % Assigns an answer to questions 
  why(Answer) :-
    progress(why, Answer).
  why(Answer) :-
    \+ progress(why, _),
    ask(why, Answer, [for_fun, i_dont_know, hungry]).
  
  fav_ing(Answer) :-
    progress(fav_ing, Answer).
  fav_ing(Answer) :-
    \+ progress(fav_ing, _),
    ask(fav_ing, Answer, [pepper,i_dont_know, cheese,vegetables,meat]).
  
  cheese(Answer) :-
    progress(cheese, Answer).
  cheese(Answer) :-
    \+ progress(cheese, _),
    ask(cheese, Answer, [hate,love,not_bad,im_a_fan]).
  
  pepper(Answer) :-
    progress(pepper, Answer).
  pepper(Answer) :-
    \+ progress(pepper, _),
    ask(smt_sweet, Answer, [yes,no]).
  
  meat(Answer) :-
    progress(meat, Answer).
  meat(Answer) :-
    \+ progress(meat, _),
    ask(meat, Answer, [hate,love,not_bad,im_a_fan]).
  
  hot(Answer) :-
    progress(hot, Answer).
  hot(Answer) :-
    \+ progress(hot, _),
    ask(hot, Answer, [hate,love,not_bad]).
  
  smt_exotic(Answer) :-
    progress(smt_exotic, Answer).
  smt_exotic(Answer) :-
    \+ progress(smt_exotic, _),
    ask(smt_exotic, Answer, [yes,no]).
  
  smt_sweet(Answer) :-
    progress(smt_sweet, Answer).
  smt_sweet(Answer) :-
    \+ progress(smt_sweet, _),
    ask(smt_sweet, Answer, [yes,no]).
  
  answers([], _).
  answers([First|Rest], Index) :-
    write(Index), write(' '), answer(First), nl,
    NextIndex is Index + 1,
    answers(Rest, NextIndex).
  
  
  parse(0, [First|_], First).
  parse(Index, [First|Rest], Response) :-
    Index > 0,
    NextIndex is Index - 1,
    parse(NextIndex, Rest, Response).
  
  
  
  ask(Question, Answer, Choices) :-
    question(Question),
    answers(Choices, 0),
    read(Index),
    parse(Index, Choices, Response),
    asserta(progress(Question, Response)),
  Response = Answer.
  