import { Component, OnInit } from '@angular/core';
import { WorkoutClient, WorkoutDto } from 'src/app/brorep-api';
import { AuthorizeService } from '../../../api-authorization/authorize.service';

@Component({
    selector: 'app-judge',
    templateUrl: './judge.component.html',
    styleUrls: ['./judge.component.scss']
})
export class JudgeComponent implements OnInit {
    step: number;
    videoUrl: string;
    judgeTypeId: string;
    workout: WorkoutDto;

  constructor(private workoutClient: WorkoutClient, private authorizeService: AuthorizeService) {}

    ngOnInit(): void {
        this.step = 0;
    }

    onGoBack() {
        this.step -= 1;
        if (this.step === 0) {
            this.judgeTypeId = null;
            this.videoUrl = null;
        }
    }

    onWorkoutSelected(workout) {
        this.step += 1;
        this.workout = workout;
    }

    onJudgeTypeSelected(id) {
        this.step += 1;
        this.judgeTypeId = id;
    }
}
