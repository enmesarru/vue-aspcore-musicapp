<template>
  <div>
    <el-row id="test">
      <el-col :span="12">
        <el-form ref="albumForm" :model="album" label-width="120px" enctype="multipart/form-data">
          <el-form-item label="Album Name" prop="name">
            <el-input v-model="album.name"></el-input>
          </el-form-item>

          <el-form-item>
            <div class="block">
              <span class="demonstration">Default</span>
              <el-date-picker
                v-model="album.releaseDate"
                type="date"
                format="dd.MM.yyyy"
                placeholder="Pick a day">
              </el-date-picker>
            </div>
          </el-form-item>

          <el-form-item>
            <input  ref="images" type="file" :rules="[{ required: true, message: 'Album image is required'}]" />
          </el-form-item>

          <el-form-item>
            <el-select v-model="album.genreId" placeholder="Select">
              <el-option
                v-for="item in genres"
                :key="item.id"
                :label="item.name"
                :value="item.id">
              </el-option>
            </el-select>
          </el-form-item>

          <el-form-item>
            <el-select v-model="album.artistId" placeholder="Select">
              <el-option
                v-for="item in artist"
                :key="item.id"
                :label="item.name"
                :value="item.id">
              </el-option>
            </el-select>
          </el-form-item>

          <el-form-item>
            <el-button type="primary" @click="submitUpload('albumForm')">Create</el-button>
            <el-button>Cancel</el-button>
          </el-form-item>
        </el-form>
      </el-col>
    </el-row>
  </div>
</template>

<script>
    export default {
      name: 'album-create',
      created () {
        this.getArtists()
        this.getGenres()
      },
      methods: {
        getArtists () {
          this.$http.get('http://localhost:5000/api/artists/')
            .then((response) => { this.artist = response.body }
              , (response) => { console.log(response.error) })
        },
        getGenres () {
          this.$http.get('http://localhost:5000/api/genres/')
            .then((response) => { this.genres = response.body }
              , (response) => { console.log(response.error) })
        },
        submitUpload (formName) {
          const file = this.$refs.images.files
          var formData = new FormData()
          formData.append('name', this.album.name)
          formData.append('releaseDate', this.album.releaseDate)
          formData.append('genreId', this.album.genreId)
          formData.append('artistId', this.album.artistId)
          if (file[0] != null) {
            formData.append('images', file[0], file[0].name)
          }
          console.log(formData)
          this.$http.post('http://localhost:5000/api/albums', formData, {
            headers: {
              'Content-Type': 'multipart/form-data; application/json; charset=utf-8;'
            }
          })
            .then((response) => { console.log(response.body) }
              , response => { console.log(response.error) })
        }
      },
      computed: {
        dateFormat () {
          let date = new Date(this.album.releaseDate).toISOString()
          this.album.releaseDate = date
        }
      },
      data () {
        return {
          artist: [],
          genres: [],
          album: {
            name: '',
            genreId: null,
            artistId: null,
            releaseDate: '',
            images: []
          },
          pickerOptions1: {
            shortcuts: [{
              text: 'Today',
              onClick (picker) {
                picker.$emit('pick', new Date())
              }
            }, {
              text: 'Yesterday',
              onClick (picker) {
                const date = new Date()
                date.setTime(date.getTime() - 3600 * 1000 * 24)
                picker.$emit('pick', date)
              }
            }, {
              text: 'A week ago',
              onClick (picker) {
                const date = new Date()
                date.setTime(date.getTime() - 3600 * 1000 * 24 * 7)
                picker.$emit('pick', date)
              }
            }]
          }
        }
      }
    }
</script>

<style scoped>

</style>
