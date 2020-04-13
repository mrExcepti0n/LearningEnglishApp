import { TrainingBase } from "../shared/models/trainingBase.model";
import { TrainingDataService } from "../shared/services/trainigData.service";
import { UserWord } from "../../vocabulary/models/word.model";
import { IQuestion } from "../shared/models/iquestion.model";
import { CollectWordQuestion } from "./collectWordQuestion.model";

export class CollectWordTraining extends TrainingBase {

  protected checkAnswerInternal(answer: string): boolean {
    let question = this.getCurrentQuestion();
    return question.translation == answer;
  }

  constructor(trainingDataService: TrainingDataService, isReverse: boolean) {
    super(trainingDataService, isReverse);
  }

  protected generateQuestions(words: UserWord[]): IQuestion[] {

    let results: IQuestion[] = [];
    for (let i = 0; i < words.length; i++) {

      let question = new CollectWordQuestion();
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
