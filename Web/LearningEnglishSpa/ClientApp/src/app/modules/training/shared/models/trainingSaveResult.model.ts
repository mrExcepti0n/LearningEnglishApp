import { TrainingType } from "./trainingType.model";
import { IQuestion } from "./iquestion.model";



export class TrainingWordSaveResult {

  constructor(question: IQuestion) {
    this.userWordId = question.userWordId;
    this.isRightAnswer = question.isRightAnswer();
    this.userAnswer = question.userAnswer;
  }
  userWordId: number;
  isRightAnswer: boolean;
  userAnswer: string;
}


export class TrainingSaveResult {

  constructor(trainingType: TrainingType, isReverseTraining: boolean, questions: IQuestion[]) {
    this.trainingType = trainingType;
    this.isReverseTraining = isReverseTraining;
    this.trainingWordResults = questions.map(q => new TrainingWordSaveResult(q));
  }
  trainingType: TrainingType;
  isReverseTraining: boolean;
  trainingWordResults: TrainingWordSaveResult[];
}

