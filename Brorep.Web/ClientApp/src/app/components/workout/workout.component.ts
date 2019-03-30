import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';

@Component({
    selector: 'app-workout',
    templateUrl: './workout.component.html',
    styleUrls: ['./workout.component.scss']
})
export class WorkoutComponent implements OnInit {

    loadThisVideo: string;
    loadVideoForm: FormGroup;

    constructor(private fb: FormBuilder) {
        this.loadVideoForm = this.fb.group({
            videoUrl: new FormControl('')
          });
    }

    ngOnInit(): void { }

    onLoadVideoFormSubmit() {
        this.loadThisVideo = this.loadVideoForm.get('videoUrl').value;
    }
}
