import { TrainingBase } from "../shared/models/trainingBase.model";
import { TrainingDataService } from "../shared/services/trainigData.service";
import { Word } from "../../vocabulary/models/word.model";
import { IQuestion } from "../shared/models/iquestion.model";
import { CollectWordQuestion } from "./collectWordQuestion.model";
import { WordImageService } from "../../shared/services/wordImage.service";

export class CollectWordTraining extends TrainingBase {

  protected checkAnswerInternal(answer: string): boolean {
    let question = this.getCurrentQuestion();
    return question.translation == answer;
  }

  constructor(trainingDataService: TrainingDataService, isReverse: boolean) {
    super(trainingDataService, isReverse);
  }

  protected generateQuestions(words: Word[]): IQuestion[] {

    let results: IQuestion[] = [];
    for (let i = 0; i < words.length; i++) {

      let question = new CollectWordQuestion();
      question.number = (i + 1).toString();
      question.word = words[i].name;
      question.translation = words[i].translation;

      let answersLetters = question.translation.split('');
      this.shuffleWords(answersLetters);

      question.letters = answersLetters;
      results.push(question);
    }

    return results;
  }
}
