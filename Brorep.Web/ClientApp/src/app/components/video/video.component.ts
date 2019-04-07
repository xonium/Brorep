import { Component, OnInit, Input, SimpleChanges, SimpleChange, EventEmitter, Output, OnChanges } from '@angular/core';
import { RenderReactPlayer } from './reactplayer.standalone';
@Component({
  selector: 'app-video',
  template: `<div class="player-wrapper">
      <div id={{videoId}} class="react-player embed-responsive embed-responsive-16by9">
      </div>
    </div>`
})
export class VideoComponent implements OnInit, OnChanges  {
  private _reactPlayer: any;

  @Input() videoId: string;

  @Input() videoUrl: string;
  private _videoUrl: string;

  @Input() videoPlay: boolean;
  private _videoPlay: boolean;

  @Input() setSeekerLocation: number;

  @Output() errorEvent = new EventEmitter();
  @Output() progressEvent = new EventEmitter();
  @Output() readyEvent = new EventEmitter();
  @Output() videoPlayEvent = new EventEmitter();
  @Output() videoPauseEvent = new EventEmitter();

  constructor() {}

  ngOnInit() {
        if (this.videoUrl) {
            const container = document.getElementById(this.videoId);
            if (!container) {
                return;
            }
            const url = this.videoUrl;
            RenderReactPlayer(container, { url, playing: this.videoPlay });
        }
    }

    ngOnChanges(changes: SimpleChanges): void {
        const container = document.getElementById(this.videoId);
        if (!container) {
            return;
        }

        const that = this;

        const videoSettings = {
            width: '100%',
            height: '100%',
            onPlay: onVideoPlay,
            onProgress: onVideoProgress,
            onError: onVideoError,
            onReady: onVideoReady,
            onPause: onVideoPause,
            ref: (player) => { this._reactPlayer = player; }
        };

        if (changes.videoUrl) {
            const inputvideoUrl: SimpleChange = changes.videoUrl;
            this._videoUrl = inputvideoUrl.currentValue;

            const url = this.videoUrl;
            this._videoPlay = true;
            RenderReactPlayer(container,
                Object.assign(videoSettings,
                    {
                        url,
                        playing: this._videoPlay
                    }));
        }

        if (changes.videoPlay) {
            const inputvideoPlay: SimpleChange = changes.videoPlay;
            this._videoPlay = inputvideoPlay.currentValue;
            const url = this._videoUrl;
            RenderReactPlayer(container,
                Object.assign(videoSettings,
                    {
                        url,
                        playing: this._videoPlay
                    }));
        }

        if (changes.setSeekerLocation) {
            const inputSeekerLocation: SimpleChange = changes.setSeekerLocation;

            if (this._reactPlayer) {
                this._reactPlayer.seekTo(inputSeekerLocation.currentValue);
            }
        }

        function onVideoProgress(progress) {
            that.progressEvent.emit(progress);
        }

        function onVideoPlay() {
            that._videoPlay = true;
            that.videoPlayEvent.emit(true);
        }

        function onVideoError(error) {
            that.errorEvent.emit(error);
        }

        function onVideoReady() {
            that.readyEvent.emit();
        }

        function onVideoPause() {
            that._videoPlay = false;
            that.videoPauseEvent.emit(true);
        }

    }
  }

