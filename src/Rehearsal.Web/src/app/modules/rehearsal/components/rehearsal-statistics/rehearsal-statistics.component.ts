import {Component, Input} from '@angular/core';
import {Rehearsal} from "../../../../types";
import RehearsalQuestionModel = Rehearsal.RehearsalQuestionModel;

@Component({
    selector: 'rehearsal-statistics',
    templateUrl: './rehearsal-statistics.component.html'
})
export class RehearsalStatisticsComponent {
    @Input() incorrectQuestions: RehearsalQuestionModel[];
}
