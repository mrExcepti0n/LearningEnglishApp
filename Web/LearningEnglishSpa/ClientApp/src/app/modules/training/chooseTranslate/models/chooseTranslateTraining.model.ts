import { UserWord } from "../../../vocabulary/models/word.model";
import { TrainingBase } from "../../shared/models/trainingBase.model";
import { TrainingService } from "../../shared/services/trainig.service";
import { IQuestion } from "../../shared/models/iquestion.model";
import { QuestionWithOptions} from "../../shared/models/questionWithOptions.model";
import { TrainingType } from "../../shared/models/trainingType.model";

export class ChooseTranslateTraining extends TrainingBase {

  constructor(trainingDataService: TrainingService, isReverse: boolean) {
    super(trainingDataService, TrainingType.ChooseTranslate, isReverse);
  }

  protected generateQuestions(words: UserWord[]): IQuestion[] {

    let results: IQuestion[] = [];
    for (let i = 0; i < words.length; i++) {

      let question = new QuestionWithOptions();
      question.userWordId = words[i].id;
      question.number = (i + 1).toString();
      question.word = words[i].word;
      question.translation = words[i].translation;

      let answer = this.getAnswers(words, i, 4);
      question.options = answer;
      results.push(question);
    }
    return results;
  }


  protected getAnswers(words: UserWord[], currentIndex: number, size: number): string[] {
    let rightAnswer = words[currentIndex].translation;
    let otherWords: UserWord[] = words.filter((w, i) => i !== currentIndex);
    this.shuffleWords(otherWords);
    let answers: string[] = otherWords.slice(0, size).map(w =>  w.translation);
    answers.push(rightAnswer);
    this.shuffleWords(answers);
    return answers;
  } 
}
