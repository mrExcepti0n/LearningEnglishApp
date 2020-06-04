import { TrainingBase } from "../shared/models/trainingBase.model";
import { TrainingService } from "../shared/services/trainig.service";
import { UserWord } from "../../vocabulary/models/word.model";
import { IQuestion } from "../shared/models/iquestion.model";
import { CollectWordQuestion } from "./collectWordQuestion.model";
import { TrainingType } from "../shared/models/trainingType.model";

export class CollectWordTraining extends TrainingBase {

  constructor(trainingDataService: TrainingService, isReverse: boolean) {
    super(trainingDataService, TrainingType.CollectWord, isReverse);
  }

  protected generateQuestions(words: UserWord[]): IQuestion[] {

    let results: IQuestion[] = [];
    for (let i = 0; i < words.length; i++) {

      let question = new CollectWordQuestion();
      question.userWordId = words[i].id;
      question.number = (i + 1).toString();
      question.word = words[i].word;
      question.translation = words[i].translation;

      const answersLetters = question.translation.split('');
      this.shuffleWords(answersLetters);

      question.letters = answersLetters;
      results.push(question);
    }

    return results;
  }
}
