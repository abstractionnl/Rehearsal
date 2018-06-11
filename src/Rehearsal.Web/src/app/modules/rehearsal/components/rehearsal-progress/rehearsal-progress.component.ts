import {Component, Input} from '@angular/core';

@Component({
  selector: 'rehearsal-progress',
  templateUrl: './rehearsal-progress.component.html'
})
export class RehearsalProgressComponent {

    @Input() public currentQuestion;
    @Input() public totalQuestions;

}
