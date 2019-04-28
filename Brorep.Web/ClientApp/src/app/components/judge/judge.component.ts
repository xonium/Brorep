import { Component, OnInit } from '@angular/core';
import { WorkoutClient, WorkoutDto } from 'src/app/brorep-api';
import { UserService } from 'src/app/services/user.service';

@Component({
    selector: 'app-judge',
    templateUrl: './judge.component.html',
    styleUrls: ['./judge.component.scss']
})
export class JudgeComponent implements OnInit {
    step: number;
    videoUrl: string;
    judgeType: string;
    workout: WorkoutDto;

    constructor(private workoutClient: WorkoutClient, private userService: UserService) {}

    ngOnInit(): void {
        this.step = 0;
    }

    onWorkoutSelected(workout) {
        this.step += 1;
        this.workout = workout;
    }

    onJudgeTypeSelected(type) {
        this.step += 1;
        this.judgeType = type;
    }
}
