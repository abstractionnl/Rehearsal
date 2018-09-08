import {Component, Input} from '@angular/core';
import {RehearsalSessionStateQuestion} from "../../store/rehearsal.state";

@Component({
    selector: 'rehearsal-statistics',
    templateUrl: './rehearsal-statistics.component.html'
})
export class RehearsalStatisticsComponent {
    @Input() incorrectQuestions: RehearsalSessionStateQuestion[];
}
